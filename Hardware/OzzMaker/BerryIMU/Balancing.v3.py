#!/usr/bin/python

import smbus, os
from LSM9DS0 import *
from Adafruit_MotorHAT import Adafruit_MotorHAT, Adafruit_DCMotor, Adafruit_StepperMotor
bus = smbus.SMBus(1)

GyroY = 0
ComboY = 0

#Stepper Motors
mh = Adafruit_MotorHAT()
StepperLeft  = mh.getStepper(0, 2)
StepperRight = mh.getStepper(0, 1)
SleepCounter = 0
Calibrating = True
CalibratingCounter = 0
BalancePoint = 0
Move = 0
Counter = 0
MoveType = Adafruit_MotorHAT.SINGLE

def writeACC(register,value):
        bus.write_byte_data(ACC_ADDRESS, register, value)
        return -1

def readACCx():
        #acc_l = bus.read_byte_data(ACC_ADDRESS, OUT_X_L_A)
        acc_h = bus.read_byte_data(ACC_ADDRESS, OUT_X_H_A)
        #acc_combined = (acc_l | acc_h <<8)
        acc_combined = acc_h
        #return acc_combined if acc_combined < 32768 else acc_combined - 65536
        return acc_combined if acc_combined < 127 else acc_combined - 256

def limit(value,limit):
        if value > limit:
                return limit
        else:
                return value
        
#initialise the accelerometer
writeACC(CTRL_REG1_XM, 0b01100111) #z,y,x axis enabled, continuos update,  100Hz data rate
writeACC(CTRL_REG2_XM, 0b00100000) #+/- 16G full scale

while True:
        
        #Read the accelerometer,gyroscope and magnetometer values
        GyroY = readACCx()
        #ComboY = int(GyroY / 50)
        #print GyroY, ComboY

        Counter += 1
        if Counter == 50:
                Counter = 0
                print GyroY #, ComboY

        if GyroY < -4 or GyroY > 4:
                SleepCounter += 1
                print "Sleeping ", SleepCounter
                if SleepCounter == 75:
                        break
        else:
                SleepCounter = 0
                BalancePoint = -1
                
                if GyroY < BalancePoint:
                        Move = 1 # limit((ComboY - BalancePoint),20)
                        for step in range(Move):
                                StepperLeft.oneStep( Adafruit_MotorHAT.FORWARD,  MoveType)
                                StepperRight.oneStep(Adafruit_MotorHAT.BACKWARD, MoveType)

                if GyroY > BalancePoint:
                        Move = 1 # limit(-(ComboY - BalancePoint),20)
                        for step in range(Move):
                                StepperLeft.oneStep( Adafruit_MotorHAT.BACKWARD, MoveType)
                                StepperRight.oneStep(Adafruit_MotorHAT.FORWARD,  MoveType)

# recommended for auto-disabling motors on shutdown!
mh.getMotor(1).run(Adafruit_MotorHAT.RELEASE)
mh.getMotor(2).run(Adafruit_MotorHAT.RELEASE)
mh.getMotor(3).run(Adafruit_MotorHAT.RELEASE)
mh.getMotor(4).run(Adafruit_MotorHAT.RELEASE)
print "Motors Released"







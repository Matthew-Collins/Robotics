#!/usr/bin/python
#Run with sudo ./motor.py -d f -s 100
#-d = direction b=back f=forward 
#-rs = Right speed 0 to 100
#-ls = Left speed 0 to 100
#Updated 14/04/2016 - Charlie Ogier

import time, argparse, initio 

parser = argparse.ArgumentParser()
parser.add_argument('-rs','--rspeed')
parser.add_argument('-ls','--lspeed')
parser.add_argument('-d','--direction')
args = parser.parse_args()

lspeed = float(args.lspeed)
rspeed = float(args.rspeed)
direction = args.direction

initio.init()

if direction == "b":
    initio.turnReverse(lspeed,rspeed)
    
if direction == "f":
    initio.turnForward(lspeed,rspeed)

time.sleep(0.25)
#initio.stop
#initio.cleanup()
    

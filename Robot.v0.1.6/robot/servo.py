from __future__ import division
import time, Adafruit_PCA9685,argparse

#Altered by Charlie Ogier 28/04/16

parser = argparse.ArgumentParser()
parser.add_argument('-s','--servon')
parser.add_argument('-p','--position')
parser.add_argument('-d','--delay')
args = parser.parse_args()

servon = int(args.servon)
position = int(args.position)
delay = int(args.delay)

# Initialise the PCA9685 using the default address (0x40).
pwm = Adafruit_PCA9685.PCA9685()

# Configure min and max servo pulse lengths
#servo_min = 150  # Min pulse length out of 4096
#servo_max = 600  # Max pulse length out of 4096

# Helper function to make setting a servo pulse width simpler.
def set_servo_pulse(channel, pulse):
    pulse_length = 1000000    # 1,000,000 us per second
    pulse_length //= 60       # 60 Hz
    pulse_length //= 4096     # 12 bits of resolution
    pulse *= 1000
    pulse //= pulse_length
    pwm.set_pwm(channel, 0, pulse)

# Set frequency to 60hz, good for servos.
pwm.set_pwm_freq(60)

pwm.set_pwm(servon, 0, position)
time.sleep(delay)


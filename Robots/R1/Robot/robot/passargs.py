#!/usr/bin/python
import argparse
 
parser = argparse.ArgumentParser()
parser.add_argument('-s','--speed')
args = parser.parse_args()
 
print (args.speed)

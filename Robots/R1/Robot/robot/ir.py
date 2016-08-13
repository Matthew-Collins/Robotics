import sys, time
import initio

initio.init()

try:
    lastL = initio.irLeft()
    lastR = initio.irRight()
    f = open('ir.txt', 'w')
    print >> f, lastL, lastR
    f.close()
    time.sleep(0.1)

finally:
    initio.cleanup()

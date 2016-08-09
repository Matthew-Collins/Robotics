import time
import initio

initio.init()
dist = initio.getDistance()
        
f = open('Sonar.txt', 'w')
print >> f, int(dist)
f.close()
time.sleep(1)
initio.cleanup()


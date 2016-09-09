if [ ! -d ~/initio ]; then
  mkdir ~/initio
fi
cd ~/initio
wget -q http://4tronix.co.uk/initio/servod.xxx -O servod
wget -q http://4tronix.co.uk/initio/initio.py -O initio.py
wget -q http://4tronix.co.uk/initio/motorTest.py -O motorTest.py
wget -q http://4tronix.co.uk/initio/motorTest2.py -O motorTest2.py
wget -q http://4tronix.co.uk/initio/irTest.py -O irTest.py
wget -q http://4tronix.co.uk/initio/servoTest.py -O servoTest.py
wget -q http://4tronix.co.uk/initio/sonarTest.py -O sonarTest.py

chmod +x servod

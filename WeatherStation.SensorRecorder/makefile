CC=g++
CFLAGS= -Wall
EXEC=WeatherStation.SensorRecorder

all:$(EXEC)

$(EXEC): i2c.o main.o
	$(CC) I2cReader.o main.o -o $(EXEC)
	rm -r *.o

main.o : i2c.o
	$(CC) -c main.cpp

i2c.o: I2cReader.h
	$(CC) -c I2cReader.h I2cReader.cpp 

clean: 
	rm -f *.o
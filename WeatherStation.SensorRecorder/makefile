CC=g++
CFLAGS= -Wall
EXEC=WeatherStation.SensorRecorder

all:$(EXEC)

$(EXEC): i2c.o main.o
	$(CC) I2cReader.o Api.o main.o -o $(EXEC)
	rm -r *.o

main.o : i2c.o api.o
	$(CC) -c main.cpp

api.o: Api.h
	$(CC) -c Api.h Api.cpp

i2c.o: I2cReader.h
	$(CC) -c I2cReader.h I2cReader.cpp 

clean: 
	rm -f *.o
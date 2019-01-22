CC=g++
CFLAGS= -Wall
EXEC=WeatherStation.SensorRecorder

all:$(EXEC)

$(EXEC): utils.o api.o i2c.o main.o
	$(CC) utils.o I2cReader.o Api.o main.o -o $(EXEC)
	rm -r *.o ; rm -f *.gch

main.o : utils.o i2c.o api.o
	$(CC) -c main.cpp

api.o: utils.o Api.h
	$(CC) -c Api.h Api.cpp

i2c.o: utils.o I2cReader.h 
	$(CC) -c I2cReader.h I2cReader.cpp 

utils.o : utils.h
	$(CC) -c utils.h utils.cpp

clean: 
	rm -f *.o ; rm -f *.gch
<p align="center"><a href="https://github.com/MaxMommersteeg/ParkingSensor" rel="nofollow"><img src="https://github.com/MaxMommersteeg/ParkingSensor/blob/master/content/setup_300x225.png"></a></p>

[![Build Status](https://maxmommersteeg.visualstudio.com/ParkingSensor/_apis/build/status/MaxMommersteeg.ParkingSensor?branchName=master)](https://maxmommersteeg.visualstudio.com/ParkingSensor/_build/latest?definitionId=4&branchName=master)

# ParkingSensor

## Disclaimer

> **WARNING**: Never ever use code from this repository to build **AND** actually use on public roads.

## Getting started (not finished)

### Prerequisites

There are different prerequisites depending on how you want to run: 
- Running just the simulation
- Running with all or several sensors

Continu under the heading that is right for you.

>Note: It is possible to run with one hardware sensor and the other being a simulated sensor.

#### Simulated
- Just your computer

#### With sensor(s)
- A Raspberry Pi
- A HC-SR04, HC-SR501 and/or Piezo Buzzer Controller
- Jumper wires (female-female, male-female, male-male)
- (optional) breadboard (for (attempting) to keep the wiring organized)

### TBD 

Getting started will be finished in a few days...

## About

### Why

This project is merely created for playing around with several cool techniques. To name a few:
- Clean Architecture
- MediatR (mediator pattern implementation for in-process communication)
- Azure pipelines (yaml)
- .NET Core self-contained deployments (apps that ship their own runtime)
- Single file applications (convienient single file app for end users)
- Assembly linking (reduced app size by trimming unused assemblies)
- .NET Core service works for creating background services

Aside from above techniques the application also leverages: logging, configuration and dependency injection.

### Functionality

Following sensors are currently supported and are working together:
 - [x] [HC-SR04 - Ultrasonic Ranging Module](https://github.com/dotnet/iot/tree/master/src/devices/Hcsr04) - For distance measurement
 - [x] [HC-SR501 - PIR Motion Sensor](https://github.com/dotnet/iot/tree/master/src/devices/Hcsr501) - For motion detection
 - [x] [Buzzer - Piezo Buzzer Controller](https://github.com/dotnet/iot/tree/master/src/devices/Buzzer)- For sound effects

## Related projects

- [.NET Core IoT Libraries](https://github.com/dotnet/iot) - Used communication with IoT devices
- [Clean architecture](https://github.com/ardalis/CleanArchitecture) - Used for creating a loosely-coupled, dependency-inverted architecture
- [MediatR](https://github.com/jbogard/MediatR) - Used for in-process messaging in application

# Betamax.Net

An attempt to create a record/playback proxy for .Net service implementations via interfaces.
It's intention is mainly to allow isolating WCF interfaces and providing previously captured data as responses.

## Unity

It will work with any IOC Container. I'm using [Unity][3] in this project, so I've added some Unity-specific goodies.

The name was inspired by the [Betamax][1] project by [Rob Fletcher][2]

[1]:http://robfletcher.github.com/betamax/
[2]:http://robfletcher.github.com
[3]:http://unity.codeplex.com/

## License

The source code is licensed under the MIT license
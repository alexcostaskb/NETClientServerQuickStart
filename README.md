Explica aquí os métodos e variables dos seguintes scripts:

- HelloWorldManager.cs
  - muestra y administra el UI
  - permite iniciar un Host, Client, o Server 
    - Host arranca un servidor y se une como cliente.
    - Client se une al servidor como cliente.
    - Server inicia un servidor.
- HelloWorldPlayer.cs
  - mueve el objeto que representa al jugador
- NetworkTransformTest.cs
  - mueve el objeto que representa al jugador en un círculo
- RPCTest.cs
  - envia llamadas al servidor

Explica os compoñentes seguintes do GameObject `NetworkManager`:

- Network Manager
  - componente de Netcode for GameObjects que tiene todos los ajustes relacionados con netcode. 
- Unity Transport
  - biblioteca para el desarrollo de juegos multijugador
- Hello World Manager
  - ver arriba

Explica os compoñentes seguintes do Prefab `Player`:

- Network Object
  - componente que permite al GameObject responder e interactuar con netcode
- Hello World Player
  - ver arriba
- Rpc Test
  - ver arriba
- Network Transform
  - componente que sincroniza el movimiento y la rotación de GameObjects a través de la red

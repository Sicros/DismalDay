# Entrega de Proyecto Final

Trabajo correspondiente al proyecto final con todos los requirimientos implementados en el juego, además de varias actualizaciones más.

A continuación se entrega el listado con cada uno de los puntos.

## Descarga Build

[https://drive.google.com/file/d/1PGytJpcyDLJMHVRQk9HUY8yNeBXwFzqo/view?usp=share_link](https://drive.google.com/file/d/1PGytJpcyDLJMHVRQk9HUY8yNeBXwFzqo/view?usp=share_link)

## Descarga UnityPackage

[https://drive.google.com/file/d/1q94s7rN4ssOsUwBetFBXfjA-yXGCjVIs/view?usp=share_link](https://drive.google.com/file/d/1q94s7rN4ssOsUwBetFBXfjA-yXGCjVIs/view?usp=share_link)

## Requerimientos proyecto

- Se mantienen los elementos pedidos en el primer entregable del proyecto.
- Tanto el personaje como los enemigos cuentan con físicas, más específicamente, RigidBodys, para evitar que al colisionar o moverse puedan caerse. También tienen colliders, además de todos los objetos del escenario (suelos, edificios, muros, mesas, entre otros).
- Existe un GameManager que se mantiene vivo a lo largo de todo el juego. También contiene componentes que son importantes para el resto de objetos y scripts que requieran acceder a esta información.
- El caso de Raycasting más notorio corresponde al de apuntado, que al colisionar con un enemigo y presionar determinados botones, se le puede ocasionar daño, simulando un disparo. También existe otro que permite reproducir un sonido de pisada, al colisionar el pie del personaje con el suelo.
- Existe varios TDA a lo largo del juego. A continuación se entrega una lista con los más importantes:
    - Inventario personaje: Los objetos y la cantidad de cada uno de estos se guardan en una lista anexa al personaje.
    - Lista de datos de guardados: Se generan listas de estructuas para guardar información sobre el inventario del personaje, las puertas que ya haya abierto y los objetos que ya han sido eliminado de los distintos escenarios. Todos estos elementos son guardados en un archivo JSON para volver a ser cargados.
- La UI del juego se utiliza con los siguientes motivos:
    - Vida: Se muestra en una esquina la vida actual y máxima del personaje.
    - Munición: Muestra la cantidad de balas que posee el personaje en su inventario, cuantas le quedan en el arma y el máximo que esta puede llevar.
    - Interacciones: Muestra mensajes de acuerdo a las interacciones que se tenga con objetos del escenario (recoger munición, abrir puertas).
    - Documentos: Se pueden leer documentos encontrados en el escenario. Estos muestran su contenido en el medio de la pantalla.
    - Otros: Existen casos más específicos, como la ventana que se muestra en el menú principal, mensajes de fin del juego, el texto inicial, entre otros.
- Se creó una escena con un menú principal, desde esta se puede iniciar un juego nuevo o revisar los controles.
- Se implementan herencias, especificamente de entidades, y a partir de estas se crean lo que son los zombies y personaje, así como también una para las armas.
- Existen eventos de C# y Unity. El primero se utiliza para llamadas entre script, como el ocasionar activar métodos de acuerdo a la vida del personaje o la munición que este lleve, mientras que el segundo está más orientado a la UI.
- Actualmente solo existe un sistema de partículas que corresponde al efecto de nieve que cae en el primer escenario del juego. Este no cubre todo el escenario, solo una pequeña porción de este el cual sigue al personaje donde vaya.
- Se utiliza Post-Processing para dar un efecto más oscuro a los bordes del juego. También se utiliza para teñir la pantalla de rojo en caso de que la vida del personaje se encuentre por debajo del 33% del total.
- Se utilizan Reflection Probes para crear un espejo en el segundo escenario del juego (MainHall) y hielo en el primer escenario.

## Ajustes adicionales

- Se implementan un total de 7 escenarios, a continuación de explica un poco de que trata cada uno de ellos.
    - MainMenu: Menú principal del juego. Desde este lugar se puede iniciar un nuevo juego o revisar sus controles.
    - Instructions: Se explica a grandes rasgos cual es la tarea a realizar en el juego.
    - Snowfield: Campo nevado muy amplio que corresponde al exterior del edificio.
    - MainHall: Corresponde al salón principal del complejo de oficinas. Desde este punto se puede ingresar a otras habitaciones.
    - MeetingRoom01: Sala de reuniones. Contiene la pista para resolver un pequeño puzzle en otra sala.
    - Office02: Oficina del complejo. Además de tener algunos enemigos, también se encuentran objetos de interés.
    - MainHall02: Segundo tramo del pasillo principal. Es el último escenario del juego.
- Ahora se puede recargar el arma presionando la tecla R. Está vienen acompañada de su animación correspondiente. Tener en cuenta que no se puede caminar mientras se recarga.
- Se mejora iluminación, dando un ambiente más oscuro, apoyando más en las luces que se encuentran en el escenario, en el que todas están son mixtas.
- Optimizada interacción entre zombies y disparo. Ya no se trabajar con listas ni updates, ya que todo este proceso se apoya en el uso de NavMeshAgent. Al realizar un disparo, todos aquellos enemigos que se encuentren en el rango, se les ajustará su nueva posición de destino e irán hasta este punto. Esta acción se detiene si es que llegan a cruzarse con el jugador o si se encuentran a menos de 3 metros de distancia con su destino.
- Interacción con objetos del escenario. Este punto se explota enormemente por medio de los inputs del jugador y el Canvas, Las interacciones permiten realizar ciertas acciones con objetos del escenario, como municiones, puertas, notas, paneles, entre otros. De acuerdo al tipo de interacción, se mostrará un mensaje en la parte inferior de la pantalla. También al revisar documentos, se mostrará su contenido en medio de la pantalla, el cual se puede navegar utilizando el scrollbar.
- Sistema de guardado. Existe un sistema de guardado con el que el jugador no puede interactura, pero si es utilizado para guardar información entre una escena y otra, para así llevar un registro de los enemigos que se han eliminado, objetos recogidos, puertas abiertas, inventario del personaje y su vida actual.

## Actualizaciones

### 2023-04-03
- Corrección de RigidBody de personaje. Se corrige problema que al apuntar hacia arriba y caminar el personaje avanza hacia al cielo.
- Se cambia estructura de objetos. Se creó un scriptable object para identificar a cada uno de los objetos y esos son agregagdos a una lista desde el cual permite que otras clases puedan acceder a cada uno de sus elementos.
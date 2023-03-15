# Desafío Post-Processing y Reflection Probes

## Enlace de descarga de UnityPackage

[https://drive.google.com/file/d/1lu2OmKsvmnhYeX2ZYRyrZ9Pz9h_kEAPN/view?usp=sharing](https://drive.google.com/file/d/1lu2OmKsvmnhYeX2ZYRyrZ9Pz9h_kEAPN/view?usp=sharing)

A continuación se entrega el detalle de cada uno de ellos.

## Post-Processing

1. Global Volume: Se agregó un Global Volume a las escenas "Scene01" y "Scene02" que agrega unos bordes negros a la pantalla con el efecto de viñeta.
2. Efecto sangrado: Se reutiliza la viñeta anterior que es controlada por un script llamado "LowHealthVignette", que la pinta de color rojo al tener una vida inferior a 4.
3. Box Volume: Al inicio de la escena "Scene02", existe un Box Volume que da un efecto de película antigua, dejando todo en escala de grises y agregando un granulado a la pantalla.

## Reflection Probes

1. Espejo: En la escena "Scene01", al inicio de esta, existe un espejo que refleja en tiempo real y en cada cuadro lo que sucede en el escenario.
2. Hielo: En la escena "Scene02", existe una pequeña pista de hielo que cumple la misma finalidad de lo mencionado anteriormente.

## Ajustes adicionales

Además de esto, también se hicieron los siguientes cambios:
- El inventario ahora posee su propio script. Anteriormente existía en el script del personaje, la que fue movida junto a sus métodos a uno propio.
- Este inventario, tenía solo 3 espacio, uno fijo para cada objeto, actualmente el personaje puede llevar hasta 10 objetos y cada uno puede ocupar cualquier lugar.
- Se agregó una nueva escena de un campo nevado. En esta se puede avanzar hasta llegar a la entrada del cuarto principal. Por ahora todo se controla por medio de muros invisibles y no hay mucho que hacer, más que recoger una caja de munición y combatir contra un zombie. Se irá actualizando en el futuro.
- Ahora, al comenzar un nuevo juego desde el menú principal, llevará al campo nevado (Scene02). Desde este se puede llegar a la escena original de la habitación avanzando y encontrando la puerta. Se puede interactuar con esta puerta presionando el botón de disparo sin estar modo de apuntar.
- Ahora existe un archivo dedicado completamente a la carga de escenas. Este script está anexado al GameManager.
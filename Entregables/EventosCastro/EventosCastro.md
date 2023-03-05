# Desafío Eventos C# y Unity

Para este desafío se trabajaron en los dos tipos de eventos: C# y Unity. Para el primero se crearon 4, mientras que para el segundo se crearon 3. Independiente del caso, cada uno viene acompañado de un "Debug.Log()" que indica el nombre del evento, la clase de origen y la clase destino.

A continuación se entrega el detalle de cada uno de ellos.

## C# Events

1. Event: onBulletReload / From: WeaponAttributes / To: CharacterEntity - Permite identificar si el arma ha sido cargada, ante esto, se descuentan el número de balas cargadas del inventario del personaje, además devuelve este valor a los atributos del arma y a la UI para que cambie el número que se muestra en pantalla.
2. Event: onBulletShot / From: WeaponAttributes / To: LaserPointer - Cada vez que se realice un disparo, devuelve si este se pudo realizar o no, para revisar si el impacto al enemigo tiene algún efecto. El evento entrego la munición actual del arma y el tiempo en que puede volver a realizar un disparo, de acuerdo a esto, el método llamado obtiene un booleano.
3. Event: onShoot / From: LaserPointer / To: WeaponAttributes - Llamada al método de disparo. Permite revisar si el arma tiene munición y, de acuerdo a esto, ve si es necesario recargar el arma o si directamente no puede y reproduce un audio de "disparo vacio". También llama al evento de unity llamado "onBulletChange".
4. Event: onBulletInventoryChange / From: CharacterEntity / To: WeaponAttributes: Evento que cumple el fin de definir una variable booleana que devuelve un true si es que el personaje tiene munición en su inventario o false en caso contrario. Este se utilizar para la recarga del arma.

## Unity Events

1. Event: onHealthChange / From: CharacterEntity / To: UIText - Utilizado para actualizar la vida actual y máxima del personaje en la UI.
2. Event: onBulletChange / From: WeaponAttributes / To: UIText - Obtiene la munición carga en el arma y su capacidad máxima para mostrarla en la UI.
3. Event: onBulletInventoryChangeUE / From: CharacterEntity / To: UIText - A partir de los datos del personaje, se obtiene la munición que lleva actualmente en su inventario para mostrarla en la UI.

## Ajustes adicionales

Además de esto, también se hicieron los siguientes cambios:
- El script de WeaponAttributes y LaserPointer fueron cambiados. Gran parte de los métodos y variables definidos en LaserPointer, pasaron a pertener a WeaponAttributes, dejando al primer script solamente con la tarea de identificar con que objeto está colisionando el raycast y si este produce daño o no.
- Todos los atributos del arma como tal, pasaron a definirse en una Scriptable Object, entre ellos se tiene el daño del arma, la capacidad máxima de munición, velocidad de disparo, entre otros valores.
- Se definieron algunos métodos para el arma que sirve unicamente para obtener los valores de alguna de sus variables.
Este es un texto de ayuda, pero no deber�a hacer falta leerlo, si lo tienes que leer, es que mi c�digo no est� como deber�a.

Counting Bits

Siguiendo el principio de SingleResponsabily, se ha creado un servicio, que devuelva los �ndices d�nde existen coincidencias con el valor "1".

Para mejorar el rendimiento, he realizado una expresi�n regular, evitando recorrer la cadena.

Como en la soluci�n se ped�a que en la primera posici�n de la lista aparecier� el numero de coincidencias, lo he abstraido del servicio, 
ya que no es su responsabilidad, y lo he llevado a la funci�n "Principal".

Como se busca una soluci�n escalable, he realizado dos m�todos de extensi�n para cadenas y enteros, conversi�n a binario, e invertir cadenas.

FraudDetection

En primer lugar, he creado un servicio que obtiene todos los pedidos fraudulentos a partir de una lista de ellas, 
evitando acoplar esto a la lectura de datos de un fichero.

La lectura de fichero la he sacado a otro servicio, y he usado un StreamReader por rendimiendo, 
ya que el ReadAllLines en grandes ficheros tiene un rendimiento mucho peor.

En los tets unitarios he moqueado la lectura de fichero, ya que si adem�s de testear la funcionalidad de pedidos fraudulentas realiza lecturas de ficheros,
estariamos probando dos cosas en lugar de una, lo ideal ser�a testear por otro lado la funcionalidad de lectura de ficheros.

Dudas:

Normalizador:
El normalizador de email, no tiene sentido.. normaliza emails v�lidos dejandolos inutilizables, he probado algun correo personal con signos de puntuaci�n y lo modifica.

Algoritmo:
No he modificado el algoritmo de comprobar las ordenes fraudulentas porque realiza algunas cosas que no he conseguido entender muy bien y no he querido cambiar su comportamiento, 
he visto que puede a�adir varias veces la misma orden fraudulenta en algunas ocasiones, no creo que eso sea lo m�s aconsejable.
Lo ideal una vez conocido el criterio utilizado, ser�a seleccionar los pedidos fraudulentos con un algoritmo de complejidad inferior al actual que es ~ O(n�)

# RetoZara
#Una persona recibe su nómina el último Jueves de cada mes. Desde el primer día que Inditex cotiza en bolsa (el 23-may-2001), ha decidido invertir cada día siguiente de haber cobrado su nómima (o si ese día no hubo cotización, el siguiente día que la haya), la cantidad de 50€ en acciones del grupo.
#La compra de las acciones la realiza al precio de apertura de ese día de compra. Esta persona utiliza un broker que le permite comprar acciones parciales de forma que pueda invertir siempre los 50€, pero este broker se queda en el momento de la compra con un 2% del importe invertido.
#Calcular cual será el capital final obtenido, si realiza la venta total de sus acciones el día 28-dic-2017 al valor del cierre de la cotización.

Pasos seguidos:
- Tener claro la estructura inicial del proyecto, separandolo por capas cada una de las funcionalidades.
- Desealizar el fichero csv para guardarlo objetos en una lista tipo Data, que recibe DateTime, decimal, decimal.
- La creación del Método GetLastFriday() es importante ya que pasandole una fecha calcula el último viernes del mes.
- Del primer listado con todos lo objetos se filtra con las fechas devueltas del método descrito, en caso de que no existiese sumase 1 día hasta que encuente una fecha con cotización.
- Todos los registro se guardan en otra lista y solo añade si no existe en la nueva lista.
- Cada registro son iterados para hacer los cálculos teniendo en cuanta el redondeo a cada valor calculado de la iteración.
- Por último la aplicación muestra la cantidad calculada con un formato de 3 decimales.

RESULTADO FINAL:
36.664,211€

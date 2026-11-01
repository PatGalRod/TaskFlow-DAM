# TaskFlow DAM - DAM2_DI_UD03_P01_Patrick_Galan

## Arquitectura

La clase `Tarea` (carpeta Models) implementa `INotifyPropertyChanged`, así que cuando cambia una propiedad, sobre todo `Completada`, la interfaz se entera sola.

El `MainViewModel` tiene una `ObservableCollection<Tarea>` con las tareas y las propiedades calculadas (progreso, texto de progreso, pendientes y estado). Está suscrito a `CollectionChanged` y al `PropertyChanged` de cada tarea, y cuando algo cambia recalcula los contadores.

La ventana usa ese ViewModel como `DataContext` y todo va con `{Binding}`: formulario, barra de progreso, lista y barra de estado. En el code-behind solo hay los `Click` de los botones, que llaman a métodos del ViewModel.

## Captura final

![Captura final](Images/Captura%20final%20App.png)

## Auditoría con IA

Modelo usado: Sonnet 5.5 Medio

# Matriz de incidencias: TaskFlow DAM

Escala de severidad de Nielsen: **4** catastrófico · **3** grave · **2** moderado · **1** cosmético.
Los hallazgos marcados con (*) deben confirmarse con Accessibility Insights, Narrator o navegación por teclado.

| ID | Vector | Hallazgo (evidencia) | Sev. | Recomendación |
|---|---|---|---|---|
| I-01 | UIA / Teclado (WCAG 2.1.1, nivel A) | Los `CheckBox` y el botón 🗑 están dentro de un `ListBox`, cuyo `TabNavigation` por defecto es `Once`. Con teclado, Tab probablemente no entra en ellos, y no se puede completar ni borrar tareas (*). | **3** (4 para usuarios solo-teclado) | `KeyboardNavigation.TabNavigation="Continue"` en el ListBox, y tecla Supr para borrar. |
| I-02 | H3 / H5 | El botón 🗑 borra al instante, sin confirmación ni deshacer. Esc también vacía el formulario sin avisar. | **3** | Confirmación, o mejor un "Deshacer" en la StatusBar. |
| I-03 | Ergonomía (Fitts) | El botón 🗑 mide unos 22 px (sin `Width/Height/Padding`) y el CheckBox de la lista unos 17 px. Ambos quedan por debajo de 24 px (WCAG 2.5.8) y lejos de los 36-44 px pedidos. | **3** | `MinWidth/MinHeight=36`, `Padding` y margen entre objetivos. |
| I-04 | UIA (*) | El `ListBoxItem` no tiene nombre propio, así que el lector probablemente anuncia el nombre de la clase del modelo para cada fila. | **3** | `ItemContainerStyle` con `AutomationProperties.Name="{Binding Titulo}"`. |
| I-05 | H1 / H6 (extra) | La fecha límite y el flag Urgente se capturan pero no se muestran en la lista. El usuario no puede verlos. | **2** | Añadir fecha e indicador de urgencia en la fila. |
| I-06 | H5 / H9 (extra) | Añadir aparece deshabilitado sin explicar por qué (campo requerido sin indicación). Un botón deshabilitado tampoco recibe foco. | **2** | Marcar el campo como obligatorio (`Título *`) y mostrar un mensaje o tooltip. |
| I-07 | UIA / Orden de foco (WCAG 2.4.3) | Tab recorre Añadir (7) antes que Limpiar (8), pero visualmente Limpiar está a la izquierda. | **2** | Invertir los `TabIndex` o el orden visual. |
| I-08 | Gestalt / H4 | Las columnas `Auto` de cada fila hacen que los badges y los botones 🗑 no se alineen entre filas (se aprecia en la captura). | **2** | `Grid.IsSharedSizeScope="True"` con `SharedSizeGroup` en las columnas. |
| I-09 | H4 / H2 (extra) | El título dice "TAREAS **ACTIVAS**", pero la lista incluye las completadas. El HelpText dice "completadas o no". | **2** | Renombrar a "Tareas" o filtrar. |
| I-10 | UIA / H1 | Los cambios en StatusBar y ProgressBar no se anuncian a los lectores de pantalla. | **2** | `AutomationProperties.LiveSetting="Polite"` en `EstadoApp` y `TextoPendientes`. |
| I-11 | UIA | Los 3 `RadioButton` no tienen contenedor semántico: el Label "Prioridad" solo apunta al primero, así que el lector no anuncia el grupo. | **2** | `StackPanel` con `AutomationProperties.Name="Prioridad"`, o `GroupBox`. |
| I-12 | Contraste no textual (WCAG 1.4.11) (*) | Borde de los inputs ≈ 2:1 contra el fondo. | **2** | Borde ≥ 3:1 (p. ej. `#757575`). |
| I-13 | H4 / feedback | La plantilla del botón Añadir no define `IsMouseOver` ni `IsPressed`, así que no hay respuesta visual al pasar el cursor ni al pulsar. | **2** | Añadir triggers de hover y presionado. |
| I-14 | H2 (extra) | Los badges "DI", "SGE" no se explican a quien no conoce las siglas. Los `HelpText` no se ven (no son ToolTip). | **2** | `ToolTip` con el nombre completo del módulo. |
| I-15 | H4 | El `Border` del progreso tiene borde negro y el GroupBox uno gris claro. | **1** | Unificar el estilo de borde. |
| I-16 | H4 | Mezcla de mayúsculas: "+ Nueva tarea" frente a "PROGRESO DEL DÍA" y "LISTADO...". | **1** | Elegir un solo criterio. |
| I-17 | H4 | Limpiar usa el estilo por defecto y Añadir uno propio. La acción primaria queda a la derecha, al revés que la convención de Windows. | **1** | Estilo homogéneo y orden de botones según la plataforma. |
| I-18 | H8 | El texto de la tarea no tiene `TextTrimming` ni `ToolTip`, y un título largo se corta. | **1** | `TextTrimming="CharacterEllipsis"` y `ToolTip`. |
| I-19 | UIA | El mnemónico `A_ñadir` (Alt+Ñ) es incómodo en teclados no españoles. | **1** | Usar otra letra. |

## Recuento por severidad

| Severidad | Nº de incidencias |
|---|---|
| 4 (catastrófico) | 0 |
| 3 (grave) | 4 |
| 2 (moderado) | 10 |
| 1 (cosmético) | 5 |
| **Total** | **19** |


| Fallo detectado | Qué he cambiado |
| :--- | --- |
| El tabulador no llegaba al resto de tareas de la lista | Añadido `KeyboardNavigation.TabNavigation="Continue"` |
| Las filas individualmente no anunciabab el título al entrar en cada fila | Añadido `<Setter Property="AutomationProperties.Name" Value="{Binding Titulo}"/>` |
| El botón 🗑 mide menos de lo requerido | Añadido MinWidth="36" MinHeight="36" al botón |
| Posibilidad de borrar una tarea por falta de dialogo de confirmación | Modificada la clase BtEliminar_Click para añadir un MessageBox de confirmación |
| Falta de contexto a la hora de leer los badges DI, SGE... si no conoces sus acrónimos | agregados tooltip a sus respectivos badges |
| Falta de color en los badges de Modulos | Agregado estilos |
| Listado de tareas activas tambien contiene tareas completadas | Cambiar nombre a Listado de tareas |

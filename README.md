# Proyecto 1: Cinemática - Trebuchet Simulator

## Descripción del Proyecto
Simulador de un trebuchet (catapulta) basado en principios de cinemática. El jugador puede ajustar fuerza y ángulo, visualizar la trayectoria del proyectil antes del lanzamiento y destruir objetivos.

### Características principales
- Cálculo de trayectoria usando **fórmulas cinemáticas** (x = vx·t, y = vy·t - ½gt²)
- Visualización de trayectoria actual + trayectoria fantasma del tiro anterior
- Cámara dinámica que sigue al proyectil desde atrás durante el vuelo
- Sistema de destrucción de objetivos
- Interfaz completa (fuerza, ángulo, contador de impactos)
- Victoria tras **2 impactos** exitosos
- Reinicio de nivel

---

## Controles

| Acción                    | Tecla                  |
|--------------------------|------------------------|
| Aumentar fuerza          | `E` o `↑` (mantener)   |
| Disminuir fuerza         | `Q` o `↓` (mantener)   |
| Aumentar ángulo          | `↑`                    |
| Disminuir ángulo         | `↓`                    |
| Lanzar proyectil         | `Space`                |
| Reiniciar proyectil      | `R`                    |

---

## Estructura del Proyecto

- **TrebuchetController.cs** - Control principal del trebuchet
- **Projectile.cs** - Física y colisiones del proyectil
- **TrajectoryLine.cs** - Trayectoria cinemática
- **GhostTrajectory.cs** - Trayectoria del tiro anterior
- **CameraFollow.cs** - Cámara dinámica
- **UIManager.cs** - Interfaz de usuario
- **GameManager.cs** - Gestión de victoria y reinicio
- **Destructible.cs** - Sistema de destrucción de objetivos

## Decisiones de Implementación

- Se utilizaron **fórmulas cinemáticas puras** para la visualización de trayectoria (requisito del enunciado).
- Sistema de **2 impactos** para ganar (más jugable que destruir completamente el puente).
- Cámara sigue al proyectil desde atrás para mejor experiencia visual.
- Uso de `Physics Material` para rebotes realistas de la piedra.
- Objetos destruidos desaparecen automáticamente con efecto de fade.

## Instrucciones para ejecutar

1. Abrir la escena `SampleScene`
2. Presionar **Play**
3. Ajustar fuerza y ángulo
4. Lanzar con `Space`
5. Destruir el objetivo con 3 impactos

**¡Gracias por revisar el proyecto!**

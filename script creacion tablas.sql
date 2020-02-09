-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Servidor: 10.2.1.115:3306
-- Tiempo de generación: 09-03-2019 a las 11:00:51
-- Versión del servidor: 10.2.16-MariaDB
-- Versión de PHP: 7.2.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `u456282986_tasks`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `CriticidadTarea`
--

CREATE TABLE `CriticidadTarea` (
  `id` int(11) NOT NULL,
  `nombre` varchar(300) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Entornos`
--

CREATE TABLE `Entornos` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ip` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `usuario` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `password` varchar(300) COLLATE utf8mb4_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `EstadosTareas`
--

CREATE TABLE `EstadosTareas` (
  `id` int(11) NOT NULL,
  `idtarea` int(11) NOT NULL,
  `idestadotarea` int(11) NOT NULL,
  `descripcion` varchar(5000) COLLATE utf8mb4_unicode_ci NOT NULL,
  `fecha` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `EstadoTarea`
--

CREATE TABLE `EstadoTarea` (
  `id` int(11) NOT NULL,
  `nombre` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `EstadoUsuario`
--

CREATE TABLE `EstadoUsuario` (
  `id` int(11) NOT NULL,
  `nombre` varchar(300) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Roles`
--

CREATE TABLE `Roles` (
  `id` int(11) NOT NULL,
  `nombre` varchar(300) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Tareas`
--

CREATE TABLE `Tareas` (
  `id` int(11) NOT NULL,
  `idusuario` int(11) NOT NULL,
  `descripcion` varchar(5000) COLLATE utf8mb4_unicode_ci NOT NULL,
  `urljira` varchar(1000) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `idtareapadre` int(11) DEFAULT NULL,
  `fecha` date NOT NULL,
  `criticidad` int(11) NOT NULL,
  `entorno` int(11) NOT NULL,
  `tipo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `TipoTarea`
--

CREATE TABLE `TipoTarea` (
  `id` int(11) NOT NULL,
  `nombre` varchar(300) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Usuarios`
--

CREATE TABLE `Usuarios` (
  `id` int(11) NOT NULL,
  `usuario` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(300) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `nombre` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `apellidos` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `rol` int(11) NOT NULL,
  `estado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Tabla de usuariios';

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `CriticidadTarea`
--
ALTER TABLE `CriticidadTarea`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `Entornos`
--
ALTER TABLE `Entornos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `EstadosTareas`
--
ALTER TABLE `EstadosTareas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FKTarea` (`idtarea`),
  ADD KEY `FKEstadoTarea` (`idestadotarea`);

--
-- Indices de la tabla `EstadoTarea`
--
ALTER TABLE `EstadoTarea`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `EstadoUsuario`
--
ALTER TABLE `EstadoUsuario`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `Roles`
--
ALTER TABLE `Roles`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `Tareas`
--
ALTER TABLE `Tareas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FKTareaPadre` (`idtareapadre`),
  ADD KEY `FKEntorno` (`entorno`),
  ADD KEY `FKTipoTarea` (`tipo`),
  ADD KEY `FKUsuario` (`idusuario`),
  ADD KEY `FKCriticidad` (`criticidad`);

--
-- Indices de la tabla `TipoTarea`
--
ALTER TABLE `TipoTarea`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `Usuarios`
--
ALTER TABLE `Usuarios`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FKroles` (`rol`),
  ADD KEY `FKestadousuario` (`estado`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `CriticidadTarea`
--
ALTER TABLE `CriticidadTarea`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `Entornos`
--
ALTER TABLE `Entornos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `EstadosTareas`
--
ALTER TABLE `EstadosTareas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `EstadoTarea`
--
ALTER TABLE `EstadoTarea`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `EstadoUsuario`
--
ALTER TABLE `EstadoUsuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `Roles`
--
ALTER TABLE `Roles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `Tareas`
--
ALTER TABLE `Tareas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `TipoTarea`
--
ALTER TABLE `TipoTarea`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `Usuarios`
--
ALTER TABLE `Usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `EstadosTareas`
--
ALTER TABLE `EstadosTareas`
  ADD CONSTRAINT `FKEstadoTarea` FOREIGN KEY (`idestadotarea`) REFERENCES `EstadoTarea` (`id`) ON UPDATE NO ACTION,
  ADD CONSTRAINT `FKTarea` FOREIGN KEY (`idtarea`) REFERENCES `Tareas` (`id`) ON UPDATE NO ACTION;

--
-- Filtros para la tabla `Tareas`
--
ALTER TABLE `Tareas`
  ADD CONSTRAINT `FKCriticidad` FOREIGN KEY (`criticidad`) REFERENCES `CriticidadTarea` (`id`) ON UPDATE NO ACTION,
  ADD CONSTRAINT `FKEntorno` FOREIGN KEY (`entorno`) REFERENCES `Entornos` (`id`) ON UPDATE NO ACTION,
  ADD CONSTRAINT `FKTareaPadre` FOREIGN KEY (`idtareapadre`) REFERENCES `Tareas` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `FKTipoTarea` FOREIGN KEY (`tipo`) REFERENCES `TipoTarea` (`id`) ON UPDATE NO ACTION,
  ADD CONSTRAINT `FKUsuario` FOREIGN KEY (`idusuario`) REFERENCES `Usuarios` (`id`) ON UPDATE NO ACTION;

--
-- Filtros para la tabla `Usuarios`
--
ALTER TABLE `Usuarios`
  ADD CONSTRAINT `FKestadousuario` FOREIGN KEY (`estado`) REFERENCES `EstadoUsuario` (`id`),
  ADD CONSTRAINT `FKroles` FOREIGN KEY (`rol`) REFERENCES `Roles` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

--Inserts de datos

INSERT INTO CriticidadTarea (nombre) VALUES ("Ninguna");
INSERT INTO CriticidadTarea (nombre) VALUES ("Baja");
INSERT INTO CriticidadTarea (nombre) VALUES ("Media");
INSERT INTO CriticidadTarea (nombre) VALUES ("Alta");
INSERT INTO CriticidadTarea (nombre) VALUES ("Crítica");
INSERT INTO Entornos (nombre) VALUES ("PRG");
INSERT INTO Entornos (nombre) VALUES ("FUN");
INSERT INTO Entornos (nombre) VALUES ("USU");
INSERT INTO Entornos (nombre) VALUES ("SIIS");
INSERT INTO EstadoTarea (nombre) VALUES ("Pendiente");
INSERT INTO EstadoTarea (nombre) VALUES ("Cancelada");
INSERT INTO EstadoTarea (nombre) VALUES ("Finalizada");
INSERT INTO EstadoUsuario (nombre) VALUES ("Activo");
INSERT INTO EstadoUsuario (nombre) VALUES ("Deshabilitado");
INSERT INTO Roles (nombre) VALUES ("Programador");
INSERT INTO Roles (nombre) VALUES ("Analista");
INSERT INTO Roles (nombre) VALUES ("Supervisor");
INSERT INTO TipoTarea (nombre) VALUES ("Evolutivo");
INSERT INTO TipoTarea (nombre) VALUES ("Correctivo");
INSERT INTO TipoTarea (nombre) VALUES ("Perfectivo");
INSERT INTO TipoTarea (nombre) VALUES ("Otros");
INSERT INTO Usuarios (usuario, password, email, nombre, apellidos, rol, estado) VALUES ("Javi","everis","","Francisco Javier","Abril",2,1);
INSERT INTO Usuarios (usuario, password, email, nombre, apellidos, rol, estado) VALUES ("Fran","everis","","Francisco Jose","Cano",2,1);
INSERT INTO Usuarios (usuario, password, email, nombre, apellidos, rol, estado) VALUES ("Ramon","everis","","Ramón","Sola",1,1);

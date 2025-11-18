// <copyright file="Program.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

JoskaShopApp app = new();
int exitCode = await app.Main(args);
Environment.Exit(exitCode);
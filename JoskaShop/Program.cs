// <copyright file="GlobalUsings.cs" company="Josef Širůčka">
// Copyright (c) Josef Širůčka. All rights reserved.
// </copyright>
// <summary>Created on: 11.03 2026</summary>

JoskaShopApp app = new();
int exitCode = await app.Main(args);
Environment.Exit(exitCode);

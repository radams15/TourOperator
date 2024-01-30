{ pkgs ? import <nixpkgs> {} }:
  pkgs.mkShell {
    nativeBuildInputs = with pkgs.buildPackages; [
      dotnet-sdk_7
      dotnet-runtime_7
      dotnet-aspnetcore_7
    ];
}


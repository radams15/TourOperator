{ pkgs ? import <nixpkgs> {} }:
  pkgs.mkShell {
    nativeBuildInputs = with pkgs.buildPackages; [
      dotnet-sdk_7
      dotnet-runtime_7
      dotnet-aspnetcore_7
    ];

    shellHook = ''
      export PATH="$PATH:/home/rhys/.dotnet/tools"
      export DOTNET_ROOT="${pkgs.dotnet-sdk_7}"
    '';
}


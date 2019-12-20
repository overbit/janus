# janus
A payment gateway solution to integrate merchants and banks

## Run Prerequisites

In order to run successfully the integration tests we need to start our mock server.
We will leverage the power of prism (mock server from Stoplight.io guys) and dynamically mock all the endpoint specified in the bank openapi3 specification.  

### Windows

From bash
```bash
./Tools/prism-cli.exe mock -d -p 55500  ../reference/Bank.v1.yaml
```
or from Powershell
```powershell
.\Tools\prism-cli.exe mock -d -p 55500 .\reference\Bank.v1.yaml
```

### MacOS

```bash
./Tools/prism-cli-macos mock -d -p 55500  ../reference/Bank.v1.yaml
```
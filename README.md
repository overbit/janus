# janus
A payment gateway solution to integrate merchants and banks

## Development environment

### Prerequisites

#### Mock server

In order to run successfully the integration tests we need to start our mock server.
We will leverage the power of prism (mock server from Stoplight.io guys) and dynamically mock all the endpoint specified in the bank openapi3 specification.

<details>
<summary>Windows</summary>

From bash
```bash
./Tools/prism-cli.exe mock -d -p 55500  ../reference/Bank.v1.yaml
```
or from Powershell
```powershell
.\Tools\prism-cli.exe mock -d -p 55500 reference\Bank.v1.yaml
```
</details> 

<details>
<summary>MacOS</summary>

```bash
./Tools/prism-cli-macos mock -d -p 55500  reference/Bank.v1.yaml
```
</details>

Configure janus application to point to the just created mock server (`http://127.0.0.1:55500`) by modifying appsetting.json as below

```json
...
  "ExternalServices": {
    "BankAPI": "http://127.0.0.1:55500"
  },
...
```
#### Tech

Since Janus is a .NET Core 3 application in order to compile the application the .NET Core 3 SDK and Runtime need to be installed.

### Compile and run

There are two options to compile and run janus: Visual Studio and dotnet CLI.

For the first, just open the solution in VS and run / debug it.

In case of missing VS, it is possible to run janus by the following command:

```bash
dotnet run --project janus
```

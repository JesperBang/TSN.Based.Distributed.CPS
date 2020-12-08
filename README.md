# TSN.Based.Distributed.CPS
Redundant routing of critical messages for a TSN-based distributed CPS

## Usage
Open the solution (.sln) in visual studio or unpack the compiled release.
* When running via Visual Studio we recommend to build the project and then navigate to the executeable in the bin folder. When running the user will be greeted by a command propt asking to chose which file to use for input.
```text
$ Please select test file: Press 1 for small.xml, 2 for medium.xml, 3 for large.xml or 4 for huge.xml 
```
The code can also run through the build in debugger in Visual Studio. For both these methods the output can be located in the source of the project 
```text
 bin -> release/debugg -> {TC0_small.solution},...,{TC7_huge.solution} 
```
The prerun solutions that we found can be seen and evaluated in the root of the project in the folder called "solutions".

* When running the code using the pre compiled executable and dll files the output {TC0_small.solution},...,{TC7_huge.solution} can be found in the same folder as the executable.

### Config
App.config contains the weighted values for the cost function {w1},...,{w4} and can be changed without recompiling the project.

## Build
```bash
# this will build the project into /bin
$ dotnet build
```

## Version Control - GitFlow (Protection)
GitFlow with the given configuration in this project has the following branches:
* **<img src="https://raw.githubusercontent.com/ModernPGP/icons/master/encryption/lock-closed.png" alt="drawing" width="15"/>Master branch:** Containing the current production code.
* **Develop branch:** Containing tested code.
* **Feature branch:** Contains code which are under development and may only be deployed to Dev and Test environments.
* **Release branch:** Contains code which have been approved by the client in the test environment and is ready to be deployed to Quality Assurance in the QA environment.

For visualisation purposes, the figure below shows the GitFlow diagram created to further explain the above bullet points.

![GitFlow](https://i.ibb.co/ykQzXGx/gitflow-trans.png)

Upon starting a new feature the developer can use the GitFlow functions of Sourcetree to create a feature branch tagged with the correct p_tagging and push this branch to origin for others to see. The feature was developed in the dev environment then tested by the client in the test environment. After getting approved the feature branch is merged into the development branch by Sourcetree. More features can be made before creating a release branch but a release branch is not required to contain multiple features. When the release branch is ready it will be deployed to QA where the client will test all the functionalities and approve the release for production where the developer will create a pull request from the release branch to master branch, this pull request can only be approved by another developer insuring code review and quality. Finally, the release branch is merged back into the development branch.

Using this approach makes it possible to develop new features parallel to each other without interference and in the end one or both features can be included in the same release branch. This also protects the master branch - All Pull requests to master must be reviewed by atleast one other contributor.

**P_Tagging**
Tagging branches with appropriate metadata is desired to keep track of branches.
* All merges must be tagged. This can easily be done with GitBash, Sourcetree or any other IDE.
* All commits must be tagged with prefix, e.g. feature/[branchname] or release/[branchnames].

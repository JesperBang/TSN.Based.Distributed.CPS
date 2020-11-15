# TSN.Based.Distributed.CPS
Redundant routing of critical messages for a TSN-based distributed CPS

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


## Installation

## Build
```bash
# this will build the project into /bin
$ dotnet build
```

## Usage

## Test

## Documentation

### CLI

### Config

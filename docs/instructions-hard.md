#Instructions - Hard

Start by “forking” (remember a “fork” is a server-side clone) the repository found at https://github.com/krist00fer/ef-03-ci-cd-hands-on by first browsing to it and then clicking the “Fork” button up on the right hand side of the root page for the GitHub repository.

Your mission is to setup a fully functional CI/CD workflow with the following requirements:

* Build pipeline should trigger on every push to the main or the development branch
* Build pipeline should trigger on new PRs
* There should be a build that runs on Linux and one that runs on Windows
* Build pipeline should report a code coverage by the unit tests
* Code changes on the development branch should trigger continuous deployment to one Linux based Web App as well as a Windows based Web App
* There should be a build badge making end user aware that the builds on the master branch succeed
* Ask for collaboration and have at least one other friend provide a PR to your repository's "development" branch in order to check that everything works as planned.
* You are not required to use Azure Pipelines to achieve the above requirements but are welcome to try other services instead.

If you have more time, feel free to implement even more complicated workflows.
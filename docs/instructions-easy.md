# Instructions - Easy

1. Start by “forking” (remember a “fork” is a server-side clone) the repository found at https://github.com/krist00fer/ef-03-ci-cd-hands-on by first browsing to it and then clicking the “Fork” button up on the right hand side of the root page for the GitHub repository.

> From now on you’ll be working with your own forked version of the original repository

2. We now want to setup Azure Pipelines for our repository. Access and setup Azure Pipelines by clicking on the Marketplace option on the top of GitHub and then search for Azure. Two options (at the time of this writing) will show up as results and select Azure Pipelines. Follow the instructions to “grant this app access to your GitHub account”, to “set up a new plan”, to “edit your plan”, etc. The steps that are required depends on whether you have used Azure Pipelines before or not.

3. At some point you’ll be asked to select your Azure DevOps Organization. Select “Create a new organization”, give your organization a new name, project name, select what datacenter to host your project within and select your project visibility to be public (for this tutorial).

4. You’ll be brought to your organization within Azure DevOps (Azure Pipelines is a feature in Azure DevOps) and are asked to select a repository. Since we are setting up a pipeline for this demo project make sure to find your repository in the list of repositories (depending on how often you have used GitHub, you might have a few or a lot of repositories to choose from).

5. Select the “Starter pipeline”. This will only give you a minimal pipeline with almost no steps. In real life you might want to start with a templated pipeline that gives you a bit more to start from… or you might like the experience of starting from the beginning.

6. Click on the “Save and run” button and in the new dialog accept the default values and once again click “Save and run” button at the bottom.

> You can see that “Save and run” will bring up a dialog with options that you might recognize from any kind of Git tool. That’s because your pipeline will be committed to your repository and therefor also version controlled.

7. Wait for the pipeline to run. If everything has gone according to plan, you have now run your first Azure Pipeline.

8. Open a new tab in your browser and head over to GitHub and your forked repository. Notice that you know have a file called `azure-pipelines.yml` in the root folder. This is the file that defines your Azure Pipelines.

9. Back in your tab with Azure DevOps, it’s time to add some more tasks to our pipeline. Find your pipeline by clicking on the “Pipelines” option in the menu on the left-hand side. Click on your pipeline and click the “Edit” Button up on the top right corner.

10. Clean up your pipeline by removing the two script steps that you got “for free” so you’re your script looks something like this

```
# Starter pipeline
# Start with a minimal pipeline that you can customize to build and deploy your code.
# Add steps that build, run tests, deploy, and more:
# https://aka.ms/yaml

trigger:
- master

pool:
  vmImage: 'ubuntu-latest'

steps:
```

11. Place your cursor on the last line and use the “Tasks Assistant” on the right-hand side to insert a `.NET Core` task and select command to be `build`


> The “Tasks Assistant” should have inserted a new task at the end like this
```
- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
```

12. Go through the procedure to save and then run the pipeline again

> Sometimes you might have to wait a while for an agent to be ready. In a real-world project with you can decide to have dedicated build agents so you don’t have to wait for your builds to succeed.

13. Add another .NET Core task to the end of your pipeline, this time select the command to be `test`. Save and run your pipeline again.

14. Once your pipeline has run to the end, let’s have a look at the results page. Each of the tasks in your pipeline are now visible in the “Logs” tab, you can click on each and every of them to get more information. Notice that there are two tasks named “DotNetCoreCLI”, one for the “build task” and one for the “test task”, this makes it somewhat hard to understand, but let’s fix it!

15. Go back and edit your pipeline definition and add the `displayName` option to your two tasks so it looks something like below. Save and re-run the pipeline again.

```
- task: DotNetCoreCLI@2
  displayName: 'Build solution'
  inputs:
    command: 'build'

- task: DotNetCoreCLI@2
  displayName: 'Test solution'
  inputs:
    command: 'test'
```

> When you now see the “logs” of your pipeline you’ll have a much easier time to see the different between the build task and the test task.


16. Now let’s have a look at some of the other “tabs” that we can find in the results view. Click on “Summary” to get a more graphically view on the progression of our pipeline and finally click on the “Tests” tab to get information how our Unit Tests worked out.


17. We are getting there our Build Pipeline seems to be doing it’s job, but we still have two more tasks to add. Go through the same steps and then add the following two step at the bottom of your build pipeline that will first publish the .NET Core build outputs to the correct folder and then publish them as build artifacts that we can later use. Save and run.

```
- task: DotNetCoreCLI@2
  displayName: 'Publish solution'
  inputs:
    command: 'publish'
    publishWebProjects: true
    arguments: --output $(Build.ArtifactStagingDirectory)

- task: PublishBuildArtifacts@1
  displayName: 'Publish Build Artifacts'
  inputs:
    PathtoPublish: '$(Build.ArtifactStagingDirectory)'
    ArtifactName: 'drop'
    publishLocation: 'Container'
```

> Notice the `arguments` property specified during the `publish` task is important for the `PublishBuildArtifacts` task to succeed. This will help us in next task

18. We have successfully built half of CI/CD, i.e. we have setup a basic Continuous Integration pipeline, that checks if our code builds and test. Wouldn’t it be nice if we could shar that knowledge to others that want to work with us or use what we’ve done. Let’s add a Status badge to our README.md file.

19. With your build pipeline selected, click on the three vertical dots you see up in the right corner of the web interface. This brings up the context menu for your pipeline. Select `Status badge`

20. Copy the Sample Markdown script and head over to your forked repository and use any way you want to edit your README.md file to include the MD-script you just copied. Save, commit, push, etc. your changes. If everything works, then you’ll see a “badge” showing if the current build succeeded or not.

21. Time to release our amazing web app to the public and let’s do it automatically every time a build succeeds. For this to happen, start by creating a new Azure Web App (preferable in the same datacenter as your pipeline runs... even though it’s not really necessary).

> This training will not guide you on how to setup an Azure Web App, please use documentation online if this is unfamiliar to you.

22. Once your Azure Web App is setup in Azure go back to your build pipelines in Azure DevOps. But instead of creating a “build pipeline” we’ll create a new “release pipeline”. Click on release in the side menu and start creating a new release pipeline.

23. You’ll be asked to select a template. Select “Azure App Service deployment” and click Apply

24. A graphical representation shows you “Artifacts” and “Stages”. A release pipeline can have many stages and be handled manually or automatically. In our case we’ll create an Continuous Deployment Release Pipeline that will trigger anytime there are new artifacts to deploy.

25. Click on the Artifacts square to add an artifact. Select `Source type` to be `Build`, pick your project in the drop down list and from `Source`select the artifacts that comes from our build pipeline, then click `Add`

26. Up on the corner of the build artifacts “square” you can see a small “flash symbol”, click on that and enable ´Continuous deployment trigger` and close the dialog once done.

27. Finally click on the square representing Stage 1 and the link saying `1 job, 1 task`

28. Fill out the missing parameters to the right such as your `Azure Subscription`, `App type` (select Web App on Windows for simplicity) and pick your `App service name` from the list. Once done, return back to your pipeline view and “Save” your changes.

> If this is the first time you connect your subscription to Azure DevOps you might have to go through the setup flow in order to link your subscription.

29. You should now successfully have setup a CD workflow and you can test it by clicking on the “Create release button”. Check that your WebApp got deployed by browsing to your web app with the added url `api/values` (i.e. `https://ef03.azurewebsites.net/api/values`. If everything worked, you would be greeted with this message.

```
["hello","world"]

```

30. Finally it’s time to check the whole workflow. Make some adjustments to your repository, using any workflow you want and make sure your web app get updated if the build succeed. Add some unit tests that fail and check what happens then.

Thank you for participating.



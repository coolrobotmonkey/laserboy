# laserboy

## Development Guide
### Setup instructions
1. Install Unity 2022.3.40f1.
2. Install Git and login. 
3. Make sure I have added you as a contributor to this project.
4. Open CMD, type git clone https://github.com/coolrobotmonkey/laserboy.git, click enter.
5. Open the folder you just cloned the repo into using Unity.
6. Start developing!

### How do I add my changes to the repo?
First of all, if somebody has pushed some changes to the repo, you need to pull the changes. Do this by using `git pull --rebase` in the console. If it tells you you have local changes, then you can type `git stash` to store your changes in the stash. If you want to get your changes back just type `git stash pop`.

To push your changes to the repo, type `git add .`, followed by `git commit -m 'some message'`, followed by `git push`. 

If git push fails due to difference between the remote and local repository, then you need to use `git pull --rebase`. There may be merge conflicts, but these can be easily solved by using a modern code editor that allows you to see incoming changes vs local changes. In most cases with merge conflicts you want to combine both sets of changes, to avoid getting rid of someone else's work.

If you have any merge conflicts you can't solve just talk to me! (nathan).
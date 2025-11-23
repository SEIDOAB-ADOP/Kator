#!/bin/bash
# Orphan the current branch. i.e. clean git history
# sudo chmod +x ./clean-branch-history.sh
# Usage: ./clean-branch-history.sh

set -e

# Get current branch name
current_branch=$(git rev-parse --abbrev-ref HEAD)

# Remove this script from git cache (if it exists)
git rm --cached clean-branch-history.sh 2>/dev/null || true

# Create orphan branch with temp name
git checkout --orphan ${current_branch}-temp

# Add all files and commit
git add .
git commit -m "Initial commit - cleaned history from branch ${current_branch}"

# Delete original branch and rename temp branch
git branch -D ${current_branch}
git branch -m ${current_branch}-temp ${current_branch}

# Get current branch name and force push it
current_branch=$(git rev-parse --abbrev-ref HEAD)
git push --force-with-lease origin $current_branch
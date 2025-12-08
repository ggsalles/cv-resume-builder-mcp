# Testing Guide

## Quick Test (Easiest)

### Step 1: Update Kiro Config

Edit `~/.kiro/settings/mcp.json`:

```json
{
  "mcpServers": {
    "cv-resume-builder": {
      "command": "python3",
      "args": ["-m", "cv_resume_builder_mcp.server"],
      "env": {
        "PYTHONPATH": "/Users/eyaabdelmoula/Desktop/devoteam_projects/poc-mpc-cv/src",
        "REPO_PATH": "/Users/eyaabdelmoula/Desktop/devoteam_projects/poc-mpc-cv",
        "AUTHOR_NAME": "eya",
        "CREDLY_USER_ID": "eya-abdelmoula",
        "LINKEDIN_PROFILE_URL": "https://www.linkedin.com/in/eya-abdelmoula"
      }
    }
  }
}
```

### Step 2: Install Dependencies

```bash
# Create a virtual environment
python3 -m venv venv
source venv/bin/activate

# Install dependencies
pip install mcp httpx pypdf

# Keep this terminal open or add to your shell profile
```

### Step 3: Restart Kiro

Close and reopen Kiro IDE.

### Step 4: Test

In Kiro chat:
```
"List available MCP tools"
```

You should see:
- get_git_log
- read_cv
- read_wins
- get_jira_tickets
- get_credly_badges
- get_linkedin_profile
- parse_cv_pdf
- generate_enhanced_cv
- get_cv_guidelines

### Step 5: Try It

```
"Get my git commits from the last month"
```

```
"Get my Credly badges"
```

## Alternative: Test with uvx (After Publishing to PyPI)

Once published to PyPI, users can use:

```json
{
  "mcpServers": {
    "cv-resume-builder": {
      "command": "uvx",
      "args": ["cv-resume-builder-mcp"],
      "env": {
        "REPO_PATH": "/path/to/repo",
        "AUTHOR_NAME": "your-name"
      }
    }
  }
}
```

No installation needed - uvx handles everything!

## Troubleshooting

### "Module not found: mcp"
Install dependencies:
```bash
python3 -m venv venv
source venv/bin/activate
pip install mcp httpx pypdf
```

### "Module not found: cv_resume_builder_mcp"
Make sure PYTHONPATH is set correctly in your config:
```json
"PYTHONPATH": "/absolute/path/to/poc-mpc-cv/src"
```

### Server not starting
Check Kiro's MCP Server view for error logs.

### Git log returns empty
Make sure AUTHOR_NAME matches your git config:
```bash
git config user.name
```

## Manual Testing (Without Kiro)

You can't easily test MCP servers manually because they use stdio protocol, but you can test the imports:

```bash
python3 -m venv venv
source venv/bin/activate
pip install mcp httpx pypdf

# Test import
PYTHONPATH=src python3 -c "from cv_resume_builder_mcp import server; print('✅ Works!')"
```

## Next Steps

Once testing works:
1. Publish to PyPI
2. Update config to use `uvx`
3. Share with others!

# API Contract: Production-ready AI YouTube Automation Platform

## Overview

This contract defines the core backend interface for user management, project creation, workflow job orchestration, and usage enforcement. The API is designed to support a .NET 8 ASP.NET Core SaaS backend and remains compatible with the repository’s layered architecture.

## Authentication

### POST /api/auth/register

**Purpose**: Create a new user account.

**Request body**:
```json
{
  "email": "creator@example.com",
  "password": "StrongPassword123!"
}
```

**Success response**:
- HTTP 201 Created
- JSON payload containing user identity and account status

**Failure response**:
- HTTP 400 Bad Request for invalid input
- HTTP 409 Conflict if the email already exists

### POST /api/auth/login

**Purpose**: Authenticate an existing user.

**Request body**:
```json
{
  "email": "creator@example.com",
  "password": "StrongPassword123!"
}
```

**Success response**:
- HTTP 200 OK with an authentication token or session reference

**Failure response**:
- HTTP 401 Unauthorized for invalid credentials

## Projects

### POST /api/projects

**Purpose**: Create a new content automation project.

**Request body**:
```json
{
  "title": "AI Fitness Shorts",
  "brief": "Create a 60-second short about beginner home workouts",
  "targetAudience": "beginners"
}
```

**Success response**:
- HTTP 201 Created
- JSON project record with status and owner metadata

### GET /api/projects/{projectId}

**Purpose**: Retrieve the current project lifecycle and metadata.

**Success response**:
- HTTP 200 OK with project details and status summary

### DELETE /api/projects/{projectId}

**Purpose**: Cancel or archive a project if allowed by policy.

**Success response**:
- HTTP 200 OK or 204 No Content

## Jobs and Workflow Execution

### POST /api/projects/{projectId}/jobs

**Purpose**: Trigger the automation workflow for the project.

**Request body**:
```json
{
  "topic": "beginner home workouts",
  "preferredFormat": "short-video"
}
```

**Success response**:
- HTTP 202 Accepted
- JSON job metadata including task ID and current status

**Failure response**:
- HTTP 402 Payment Required if the user exceeds plan limits
- HTTP 403 Forbidden if the user cannot access the project
- HTTP 409 Conflict if a conflicting job is already active

### GET /api/projects/{projectId}/jobs/{jobId}

**Purpose**: Retrieve current execution status, any errors, and the latest stage information.

**Success response**:
- HTTP 200 OK with task status, stage list, and result references

## Usage and Billing

### GET /api/users/me/usage

**Purpose**: Show remaining quota and recent consumption.

**Success response**:
- HTTP 200 OK with usage counters and plan metadata

## Error Contract

All APIs must return a structured error object when possible:

```json
{
  "error": {
    "code": "USAGE_LIMIT_EXCEEDED",
    "message": "This project exceeds your current plan limits.",
    "retryable": false
  }
}
```

This contract provides the critical external interface for the initial production-ready MVP while keeping implementation details internal to the service layer.

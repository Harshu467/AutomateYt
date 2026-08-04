# Quickstart Validation Guide

## Prerequisites

- Docker Desktop or Docker Engine installed
- .NET 8 SDK available for local builds and tests
- Access to the repository root

## Setup

1. Open a terminal in the repository root.
2. Run the existing container stack:
   `docker compose up --build`
3. Confirm the API container starts successfully and the Postgres and Redis containers are healthy.

## Validation Scenarios

### Scenario 1: API boots and serves a baseline response

**Command**:
`dotnet build backend/API/API.csproj`

**Expected outcome**:
- Build completes without errors.
- The API project is ready for controller and service work.

### Scenario 2: User account and project creation flow

**Manual validation**:
- Use the API to create a user account.
- Create a project for a target YouTube topic and brief.
- Confirm project metadata is stored and accessible.

**Expected outcome**:
- A project record appears in the data store with the correct owner and status.
- The user sees a valid project representation through the API.

### Scenario 3: Workflow execution and status tracking

**Manual validation**:
- Create a project with a valid brief.
- Start the content generation workflow.
- Inspect the resulting task records and stage transitions.

**Expected outcome**:
- Tasks move through queued, running, and completion or failure states.
- Error and retry metadata remain visible to an operator.

### Scenario 4: Quota enforcement

**Manual validation**:
- Use a user account at or beyond the limit for generation actions.
- Trigger the expensive generation path.

**Expected outcome**:
- The system blocks or defers the workflow with an explicit limit message.
- The denial is recorded as a usage or access policy event.

## Expected Result

The feature is ready for implementation when these scenarios can be executed end-to-end without silent failures, ambiguous statuses, or missing user ownership boundaries.

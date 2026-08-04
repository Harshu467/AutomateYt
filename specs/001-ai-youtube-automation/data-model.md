# Data Model: Production-ready AI YouTube Automation Platform

## Core Entities

### User

**Purpose**: Represents an authenticated SaaS account and owns projects, usage, and access control.

**Fields**:
- Id: unique user identifier
- Email: unique login identifier
- PasswordHash: secure hash of the user password or equivalent credential material
- Plan: subscription tier or plan name
- CreatedAt: account creation timestamp
- UpdatedAt: last modification timestamp

**Relationships**:
- One user has many projects
- One user has many usage records

**Validation rules**:
- Email must be unique and valid
- Password hash must never be stored in plaintext
- Plan must map to a known tier definition

### SubscriptionPlan

**Purpose**: Represents a billing or access tier that governs feature usage and limits.

**Fields**:
- Id
- Name
- MaxProjects
- MaxVideoTasksPerMonth
- AllowedFeatures
- IsActive

**Relationships**:
- One plan can be assigned to many users

**Validation rules**:
- Plan names must be unique
- Limits must be non-negative

### Project

**Purpose**: Encapsulates a single content automation initiative.

**Fields**:
- Id
- UserId
- Title
- Brief
- TargetAudience
- Status
- CreatedAt
- UpdatedAt

**Relationships**:
- Many projects belong to one user
- One project has many video tasks
- One project has many generated assets

**Validation rules**:
- Title and brief must not be empty for active workflows
- Project status must remain in a defined state machine (Draft, Queued, Running, Completed, Failed, Cancelled)

### VideoTask

**Purpose**: Represents a specific execution step or job in the AI video pipeline.

**Fields**:
- Id
- ProjectId
- UserId
- Topic
- Stage
- Status
- RetryCount
- ErrorMessage
- InputPayload
- OutputPayload
- CreatedAt
- UpdatedAt
- CompletedAt

**Relationships**:
- Many tasks belong to one project
- One task may produce one or more generated assets

**Validation rules**:
- Stage and status must be from known enumerations
- Retry count must be non-negative
- Failed tasks must store a meaningful error payload

### GeneratedAsset

**Purpose**: Represents output documents or media files created by a project stage, such as a script, voice file, or rendered video.

**Fields**:
- Id
- ProjectId
- TaskId
- AssetType
- StorageUrl
- Metadata
- CreatedAt

**Relationships**:
- Many assets belong to one project
- Many assets are produced by one task

**Validation rules**:
- Asset type must be a supported value
- Storage URL must be provided when the asset is persisted successfully

### UserUsage

**Purpose**: Records consumption against the plan, helping enforce quotas and monitor costs.

**Fields**:
- Id
- UserId
- UsageType
- Quantity
- RecordedAt
- ContextRef

**Relationships**:
- Many usage records belong to one user

**Validation rules**:
- Quantity must be positive when recorded
- Usage type must map to a recognized billing category

## State Transitions

### Project lifecycle
- Draft → Queued → Running → Completed
- Draft → Queued → Running → Failed
- Any active state → Cancelled

### Task lifecycle
- Pending → Queued → Running → Succeeded
- Pending → Queued → Running → Failed
- Failed → Queued (on retry)
- Running → Cancelled if the job is intentionally stopped

## Notes

This data model preserves the constitution’s requirements for traceability, secure access, and usage accountability while remaining compatible with the existing .NET architecture and Docker deployment model.

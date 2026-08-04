# Research: Production-ready AI YouTube Automation Platform

## Research Topics

### 1. Architecture fit for the repository

**Decision**: Keep the repository’s layered .NET structure and add focused application services for auth, usage tracking, job orchestration, and provider integration. The API layer will be the public entry point, while Domain and Application layers remain independent of HTTP and storage concerns.

**Rationale**: The codebase already contains Domain entities, Application services, and infrastructure context; this map aligns with the constitution’s separation-of-concerns requirement and the existing Docker-based workflow.

**Alternatives considered**:
- Full-service split into isolated worker services: rejected for the initial release because it adds operational complexity without user-visible value.
- Embedding generation logic directly in controllers: rejected because it violates the project’s layering and traceability standards.

### 2. Data persistence strategy

**Decision**: Use PostgreSQL as the canonical persistence store for users, projects, usage records, and tasks, with EF Core managing relational models. Redis is used for queue coordination or cache needs, not as the system of record.

**Rationale**: The product requires durable workflow history and structured usage enforcement; PostgreSQL is a better fit than in-memory storage for production reliability and auditability.

**Alternatives considered**:
- In-memory EF data only: rejected because it cannot support production state, retry recovery, or account integrity.
- No SQL store and file-only metadata: rejected because workflow records and quotas need structured querying and relational integrity.

### 3. External provider integration model

**Decision**: Treat each provider—OpenAI, voice service, media processing, and YouTube upload—behind a dedicated interface with explicit contracts and failure states. The pipeline service orchestrates these calls, while provider clients handle raw API logic and retries.

**Rationale**: The constitution requires traceability and cost control. A provider abstraction allows validation, quotas, and failure handling without coupling the domain model to provider-specific implementations.

**Alternatives considered**:
- Direct calls from controllers: rejected because it erodes retry safety and makes job troubleshooting harder.
- One large monolithic service encapsulating all integrations: rejected because it reduces testability and increases blast radius.

### 4. Job execution model

**Decision**: Use persistent workflow tasks with explicit stages and statuses. A job runner or background worker executes pipeline steps, records state transitions, and isolates failures to a specific stage rather than failing the entire user project silently.

**Rationale**: Production readiness depends on meaningful status tracking and recoverability rather than purely synchronous one-shot generation.

**Alternatives considered**:
- Synchronous generation in the API request: rejected because it makes users wait for long-running jobs and complicates provider timeouts.
- No queued model and only in-process execution: rejected because it creates poor resilience and no real job history.

### 5. SaaS access and quota enforcement

**Decision**: Use a simple but enforceable plan model with user-level usage tracking and a permission boundary around project access and generation actions. Quota checks occur before executing expensive tasks, and the system records usage after successful or partially successful actions as appropriate.

**Rationale**: The constitution explicitly requires reliability and cost control, and the feature spec includes restrictions and secure access to user-owned projects.

**Alternatives considered**:
- Allowing unlimited generation for all accounts: rejected because it creates cost risk and makes the business model unsustainable.
- Enforcing quotas only after job execution: rejected because it allows runaway costs before intervention. 

## Final Decisions

- Adopt a .NET 8 ASP.NET Core SaaS backend using the existing API/Application/Domain/Infrastructure split.
- Persist relational data in PostgreSQL and use Redis for queue or cache coordination.
- Abstract external AI/media integrations behind interfaces with explicit failure handling.
- Implement workflow jobs as durable stage-based tasks with event or status transitions.
- Enforce per-user access and quota controls for secure, sustainable usage.

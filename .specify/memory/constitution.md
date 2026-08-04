<!--
Sync Impact Report:
- Version change: N/A → 1.0.0
- Modified principles: N/A → 5 project-specific principles
- Added sections: Additional Constraints, Development Workflow
- Removed sections: N/A
- Follow-up TODOs: None
-->
# AutomateYt Constitution

## Core Principles

### I. User Value Before Optimization
AutomateYt MUST prioritize customer-facing outcomes over speculative internal complexity. Features must directly support script generation, voice synthesis, video assembly, upload automation, or SaaS billing/account management, and any additional work must have a documented business or operational justification.

Rationale: This product is a workflow automation system for creators, so every change must improve delivery speed, reliability, or user trust rather than add abstract infrastructure without a measurable benefit.

### II. AI Workflows Must Be Traceable and Safe
Every automated step involving model output, generated scripts, voice synthesis, or media rendering MUST be observable, reviewable, and recoverable. The system MUST log job inputs, status transitions, prompts or generation parameters where applicable, and failure points so operators can debug or rollback without ambiguity.

Rationale: AI-driven media production has irreversible downstream effects; traceability is the minimum requirement for safe automation and operational trust.

### III. Quality and Testing Are Non-Negotiable
No feature or bug fix may merge without the relevant tests being added or updated first. The project MUST validate business-critical paths with automated tests, especially auth flows, usage limits, pipeline execution, and integration points with external services such as OpenAI, ElevenLabs, and YouTube APIs.

Rationale: The platform depends on multi-step external integrations; untested automation fails silently at the worst time and creates costly retries or content quality issues.

### IV. Reliability and Cost Control Are Architectural Requirements
The platform MUST treat payments, job scheduling, rate limits, retries, and usage tracking as first-class concerns. Services that call paid or quota-based APIs MUST enforce limits, fail gracefully, and surface actionable errors when usage caps or provider failures occur.

Rationale: Media generation is resource-intensive and costly; the system must protect both the business and the user from runaway expenses and unreliable execution.

### V. Security, Privacy, and Responsible Usage
User data, credentials, and generated content MUST be handled with least-privilege access, secure storage, and explicit authorization boundaries. Secrets MUST NOT be committed to source control, and access to user accounts, billing data, and generated media MUST be restricted to the minimum needed for each service or operator.

Rationale: SaaS automation handles creative work and sensitive account data, so security controls are part of product correctness rather than optional hardening.

## Additional Constraints

The project MUST use the .NET backend and Docker-based deployment model described in the repository, and changes must remain compatible with the existing containerized development workflow. New services, interfaces, and integrations MUST preserve clear separation between domain logic, application services, infrastructure concerns, and external provider adapters.

The system MUST treat external APIs as unreliable by default. Authentication, retries, timeouts, exception handling, and usage accounting must be designed as part of the core workflow rather than as afterthoughts. Any feature that touches script generation, video rendering, or uploads must validate the success path and failure path before release.

## Development Workflow

All work MUST follow a disciplined delivery flow: define the problem, implement the smallest viable change, validate the relevant behavior with tests, and review the result for security, reliability, and operational impact. Pull requests MUST describe the user effect, operational risks, and any external dependency changes.

Changes that affect user memberships, payments, job processing, or content generation MUST be reviewed for compliance with this constitution before merge. The team MUST keep the repository in a deployable state and avoid leaving source control, configuration, or environment assumptions in a broken or undocumented condition.

## Governance

This Constitution supersedes ad hoc engineering practices for this repository. Any amendment requires a documented change to this file, a clear rationale for the governing rule being added or changed, and review by the maintainers before the rule is treated as binding.

The project MUST review constitutional compliance during pull-request review and at the start of significant feature work. Non-compliance is a project-level concern and must be corrected before merge if it affects correctness, security, cost control, or reliability.

Versioning follows semantic versioning: MAJOR for backward-incompatible governance changes, MINOR for substantive new principles or expanded requirements, and PATCH for clarifications, wording fixes, and non-semantic refinements. This document is the authoritative source for project governance and must remain aligned with the actual runtime architecture and operational practices.

**Version**: 1.0.0 | **Ratified**: 2026-08-04 | **Last Amended**: 2026-08-04

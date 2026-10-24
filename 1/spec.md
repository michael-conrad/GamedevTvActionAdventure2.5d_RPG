# Spec — add `.opencode/` ignore pattern to `.gitignore`

- **Repo**: `GamedevTvActionAdventure2.5d_RPG`
- **Stage**: spec (awaiting the developer's approval for plan/implement)

## Problem

This repository carries no local `.opencode` agent deck. After cloning
https://github.com/michael-conrad/.opencode.git into `.opencode/` (full clone, per developer directive), git would
see the nested repository's files as untracked noise that could be staged
by accident. `.gitignore` must cover the path so the deck stays untracked
and `git status` stays clean.

## Scope

- `.gitignore`: the pattern is added to the existing .gitignore
- `.opencode/` is NOT part of this change: the deck is a full local clone and
  is deliberately untracked (local-authoritative deck convention).

## Requirements

- R-1: `.gitignore` SHALL contain a pattern matching `.opencode/`.
- R-2: git SHALL resolve `.opencode/` paths as ignored (`git check-ignore` passes).
- R-3: `git status --porcelain` SHALL NOT list any `.opencode/` entries while
  the local deck clone exists.
- R-4: The change set SHALL leave every other pending working-tree change
  untouched (stage `.gitignore` only).

## Impact

- Repo-local hygiene only; no source, build, or runtime effect.
- Risk: minimal, an additive ignore pattern; the deck stays local-only.

## Approach

Clone the deck repository fully into `.opencode/`, add the `.opencode/`
pattern to `.gitignore`, commit on a feature branch, push, and review via
PR. The issue reserves the number and anchors the spec.

## Success criteria

- SC-1 (structural): `.gitignore` matches `.opencode/`; verified via
  `git check-ignore -q .opencode/AGENTS.md` (exit 0).
- SC-2 (structural): no `.opencode/` lines in `git status --porcelain` while
  the deck clone exists; execute `git status --porcelain` and inspect.

## Out of scope

- converting `.opencode/` to a git submodule
- any edit inside `.opencode/` deck content
- trunk syncs, merges, or other repositories' work

## Success-criteria table

| ID | Criterion | Class | Instrument |
|----|-----------|-------|------------|
| SC-1 | `.gitignore` matches `.opencode/` | structural | `git check-ignore -q .opencode/AGENTS.md` (exit 0) |
| SC-2 | clean status with deck clone present | structural | `git status --porcelain` has no `.opencode/` lines |

`.issues/1/` (local-authoritative store)

---

🤖 Co-authored with AI: OpenCode (huggingface/zai-org/GLM-5.3-Flash)

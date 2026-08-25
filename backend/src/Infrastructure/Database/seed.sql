-- Sample data for local development. Safe to run repeatedly (ON CONFLICT DO NOTHING) and safe to
-- commit -- everything here is fake, there are no real credentials or personal data.
--
-- Run after `dotnet ef database update` has created the schema:
--   & "C:\Program Files\PostgreSQL\<version>\bin\psql.exe" -U postgres -h localhost -d escape_database -f backend/src/Infrastructure/Database/seed.sql

INSERT INTO priority (priority_id, priority_name) VALUES
    (1, 'Low'),
    (2, 'Medium'),
    (3, 'High')
ON CONFLICT (priority_id) DO NOTHING;

INSERT INTO status (status_id, status_name) VALUES
    (1, 'To Do'),
    (2, 'In Progress'),
    (3, 'Completed')
ON CONFLICT (status_id) DO NOTHING;

INSERT INTO position_level (level, position) VALUES
    (1, 'Junior Developer'),
    (2, 'Senior Developer'),
    (3, 'Project Manager')
ON CONFLICT (level) DO NOTHING;

INSERT INTO projects (project_id, name_, description, priority_id, status_id, start_date, end_date) VALUES
    ('PRJ001', 'Website Redesign', 'Refresh the marketing site', 3, 2, '2026-01-01', '2026-03-01'),
    ('PRJ002', 'Internal Tooling', 'Developer productivity tools', 2, 1, '2026-01-05', '2026-06-01')
ON CONFLICT (project_id) DO NOTHING;

INSERT INTO tasks (task_id, name_, description, priority_id, start_date, end_date, status_id, project_id, parent_task) VALUES
    ('TSK001', 'Design homepage mockup', 'Create Figma mockup for homepage', 2, '2026-01-05', '2026-01-15', 3, 'PRJ001', NULL),
    ('TSK002', 'Implement homepage', 'Build homepage in React', 3, '2026-01-16', '2026-02-01', 2, 'PRJ001', 'TSK001'),
    ('TSK003', 'Set up CI pipeline', 'Configure GitHub Actions for build/test', 2, '2026-01-10', '2026-01-12', 1, 'PRJ002', NULL)
ON CONFLICT (task_id) DO NOTHING;

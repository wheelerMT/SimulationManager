from pathlib import Path

import pytest

from src.job_runner import JobRunner


class TestJobRunner:
    """Tests the JobRunner class."""

    def test_job_dir_returns_correct_path(self):
        """Tests that the job_dir property returns the correct path."""
        job_path = Path("./")
        job_runner = JobRunner(job_path)

        assert job_runner.job_dir == job_path

    def test_job_dir_fails_with_nonexistent_path(self):
        """Tests that the job_dir property fails with a non-existent path."""
        job_path = Path("./not_a_directory")
        with pytest.raises(ValueError):
            JobRunner(job_path)

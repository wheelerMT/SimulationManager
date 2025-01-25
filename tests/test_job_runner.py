import os
from pathlib import Path

import pytest

from src.job_runner import JobRunner


class TestJobRunner:
    """Tests the JobRunner class."""

    @pytest.fixture
    def job_runner(self):
        """Return a basic instance of the JobRunner class."""
        return JobRunner(Path("./"))

    def test_job_dir_returns_correct_path(self, job_runner):
        """Tests that the job_dir property returns the correct path."""
        assert job_runner.job_dir == Path("./")

    def test_job_dir_fails_with_nonexistent_path(self):
        """Tests that the job_dir property fails with a non-existent path."""
        fake_job_path = Path("./not_a_directory")
        with pytest.raises(ValueError):
            JobRunner(fake_job_path)

    def test_max_running_jobs_returns_max_cpu_count(self, job_runner):
        """Tests that max_running_jobs returns the maximum number of cores available."""
        assert job_runner.max_running_jobs == os.cpu_count()

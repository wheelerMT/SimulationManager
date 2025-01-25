import os
import platform
from pathlib import Path

import pytest

from src.job_runner import JobRunner


class TestJobRunner:
    """Tests the JobRunner class."""

    os_type = platform.system().lower()
    match os_type:
        case "windows":
            script_dir = Path(__file__).parent / "data/processes/windows/"
        case _:
            script_dir = Path(__file__).parent / "data/processes/unix/"

    @pytest.fixture
    def job_runner(self) -> JobRunner:
        """Return a basic instance of the JobRunner class.

        The job directory is automatically set to what is specified by `script_dir`.
        """
        return JobRunner(self.script_dir)

    def test_job_dir_returns_correct_path(self, job_runner):
        """Tests that the job_dir property returns the correct path."""
        assert job_runner.job_dir == self.script_dir

    def test_instantiation_fails_with_nonexistent_path(self):
        """Tests that instantiation fails with a non-existent path."""
        fake_job_path = Path("./not_a_directory")
        with pytest.raises(ValueError):
            JobRunner(fake_job_path)

    def test_core_count_returns_max_cpu_count(self, job_runner):
        """Tests that core_count returns the maximum number of cores available."""
        assert job_runner.core_count == os.cpu_count()

    def test_find_jobs_finds_all_eligible_jobs(self):
        """Tests that all eligible jobs are found upon initialization."""
        job_runner = JobRunner(self.script_dir)
        num_of_jobs = len(os.listdir(self.script_dir))
        assert job_runner.job_queue.qsize() == num_of_jobs

    def test_find_jobs_with_junk_job_dir_has_empty_queue(self):
        """Tests that a directory with no eligible jobs has an empty queue."""
        job_runner = JobRunner(self.script_dir / "../junk")

        assert job_runner.job_queue.empty()

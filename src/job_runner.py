import os.path
from os import PathLike
from queue import Queue


class JobRunner:
    """Run jobs (executables, etc.) simultaneously in a given directory."""

    def __init__(self, job_dir: PathLike):
        """Initialize a new instance of the JobRunner class.

        :param job_dir: The directory where jobs are stored.
        """
        self.job_dir = job_dir
        self._job_queue = Queue()
        self._find_jobs_in_dir()
        self._core_count = os.cpu_count()

    @property
    def job_dir(self) -> PathLike:
        """Get the directory where jobs are stored."""
        return self._job_dir

    @job_dir.setter
    def job_dir(self, value: PathLike) -> None:
        """Set the directory where jobs are stored."""
        if os.path.exists(value):
            self._job_dir = value
        else:
            raise ValueError(f"Directory {value} does not exist.")

    @property
    def job_queue(self) -> Queue:
        """Get the job queue."""
        return self._job_queue

    @property
    def core_count(self) -> int | None:
        """Get the maximum number of jobs that can be run concurrently."""
        return self._core_count

    def _find_jobs_in_dir(self) -> None:
        """Find eligible jobs in the job directory and add them to the queue."""
        for file in os.listdir(self.job_dir):
            if file.endswith((".bat", ".sh", ".exe", ".ps1")):
                self._job_queue.put(os.path.join(self.job_dir, file))

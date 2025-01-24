import os.path
from os import PathLike


class JobRunner:
    """Runs jobs (executables, etc.) simultaneously in a given directory."""

    def __init__(self, job_dir: PathLike):
        """Initialize a new instance of the JobRunner class.

        :param job_dir: The directory where jobs are stored.
        """
        self.job_dir = job_dir

    @property
    def job_dir(self):
        """Gets the directory where jobs are stored."""
        return self._job_dir

    @job_dir.setter
    def job_dir(self, value):
        """Set the directory where jobs are stored."""
        if os.path.exists(value):
            self._job_dir = value
        else:
            raise ValueError(f"Directory {value} does not exist.")

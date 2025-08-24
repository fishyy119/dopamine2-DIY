import re
import shutil
import subprocess
from pathlib import Path
from typing import List, TextIO


target_revision: int = 3
ROOT_PATH: Path = Path(__file__).resolve().parent
change_count: int = 0  # 一共有4个字符串需要修改，此变量用于校验

# 各种文件路径
file_sln: Path = ROOT_PATH / "Dopamine.sln"
file_assembly: Path = ROOT_PATH / "Dopamine/SharedAssemblyInfo.cs"
file_infoXAML: Path = ROOT_PATH / "Dopamine/Views/FullPlayer/Information/InformationAbout.xaml"

# 文件夹路径
bin_packager: Path = ROOT_PATH / "Dopamine.Packager/bin/Release"
bin_main: Path = ROOT_PATH / "Dopamine/bin/Release"
folder_output: Path = Path("C:/Temp/Dopamine")
# 可执行文件路径
packager_exe: Path = bin_main / "Dopamine.Packager.exe"
MSBuild_exe: Path = Path("D:/Program Files/Microsoft Visual Studio/2022/Community/MSBuild/Current/Bin/MSBuild.exe")


def change_version(file_path: Path, new_revision: int) -> int:
    """查找文件中的版本号，并修改最后一位"""
    change_count: int = 0
    try:
        with file_path.open("r", encoding="utf-8") as f:
            content: str = f.read()
            content_origin = content

        for match in re.finditer(r"(2\.0\.10\+koifish\.)[0-9]+", content_origin):
            old_version: str = match.group(0)
            old_suffix: str = match.group(1)
            new_version: str = f"{old_suffix}{new_revision}"
            content = content.replace(old_version, new_version, 1)
            change_count += 1
            print(f"{file_path}: ", end="")
            styled_print(f"{old_version} → {new_version}", styles=[Style.GREEN])

        for match in re.finditer(r"(2\.0\.10\.5)[0-9]+", content_origin):
            old_version: str = match.group(0)
            old_suffix: str = match.group(1)
            new_version: str = f"{old_suffix}{new_revision:03d}"
            change_count += 1
            content = content.replace(old_version, new_version, 1)

            print(f"{file_path}: ", end="")
            styled_print(f"{old_version} → {new_version}", styles=[Style.GREEN])

        with file_path.open("w", encoding="utf-8") as f:
            f.write(content)

    except FileNotFoundError:
        styled_print(f"File {file_path} is not found", styles=[Style.RED])

    return change_count


def compile_project() -> None:
    """调用 MSBuild 进行编译"""
    file_buildlog = ROOT_PATH / "build_output.log"
    print("\nCompiling ... ", end="", flush=True)
    try:
        with open(file_buildlog, "w") as log:
            subprocess.run(
                [str(MSBuild_exe), str(file_sln), "/p:Configuration=Release", "/p:Platform=Any CPU"],
                stdout=log,
                stderr=subprocess.STDOUT,
                check=True,
            )
        styled_print("OK", styles=[Style.GREEN])
    except subprocess.CalledProcessError as e:
        styled_print(f"\nFailed: {e}\nSee log: {file_buildlog}", styles=[Style.RED])
        exit(2)


def copy_files() -> None:
    """将 folder1 中的文件复制到 folder2"""
    try:
        print("Copying ... ", end="", flush=True)
        shutil.copytree(bin_packager, bin_main, dirs_exist_ok=True)
        styled_print("OK", styles=[Style.GREEN])
    except Exception as e:
        styled_print(f"\nFailed: {e}", styles=[Style.RED])
        exit(3)


def run_packager() -> None:
    """执行 Packager.exe 并手动输入"""
    print("Runing Packager.exe ... ", end="", flush=True)
    if not packager_exe.exists():
        styled_print("\nFailed: can't find Packager.exe", styles=[Style.RED])
        exit(4)

    styled_print("\nManipulate in a new window", styles=[Style.YELLOW])
    try:
        subprocess.Popen(f'start cmd /k "{str(packager_exe)}"', cwd=bin_main, shell=True)
        input("Press enter to rename build output.")

    except subprocess.CalledProcessError as e:
        styled_print(f"\nFailed: {e}", styles=[Style.RED])
        exit(4)


def rename_files() -> None:
    """重命名 folder_output 中的 .msi .zip 文件"""
    try:
        print("Renaming ... ", end="", flush=True)
        for file_output in folder_output.iterdir():
            name_old = file_output.name
            name_new = re.sub(r"\b2\.0\.10\b", f"2.0.10+koifish.{target_revision}", name_old)
            file_output.rename(file_output.with_name(name_new))
        styled_print("OK", styles=[Style.GREEN])
    except Exception as e:
        styled_print(f"\nFailed: {e}", styles=[Style.RED])


class Style:
    BLACK = "\033[30m"
    RED = "\033[31m"
    GREEN = "\033[32m"
    YELLOW = "\033[33m"
    BLUE = "\033[34m"
    MAGENTA = "\033[35m"
    CYAN = "\033[36m"
    WHITE = "\033[37m"
    UNDERLINE = "\033[4m"
    RESET = "\033[0m"


def styled_print(
    *values: object,
    styles: List[str] | None = None,
    sep: str = " ",
    end: str | None = "\n",
    file: TextIO | None = None,
    flush: bool = False,
):
    style_prefix = "".join(styles) if styles else ""
    text = sep.join(map(str, values))
    print(f"{style_prefix}{text}{Style.RESET}", end=end, file=file, flush=flush)


if __name__ == "__main__":
    # 修改文件中的版本号
    change_count: int = 0
    change_count += change_version(file_assembly, target_revision)
    change_count += change_version(file_infoXAML, target_revision)
    if change_count != 4:
        print("版本号修改次数不正确，即将退出")
        exit(1)

    # 调用编译工具
    compile_project()

    # 将packager的编译结果复制到main下运行，并且对结果重命名
    copy_files()
    run_packager()
    rename_files()

# Prompt the user to enter a name
$name = Read-Host "Enter a name"

# Define the base folder name
$baseFolderName = "${name}Features"

# Get the current directory
$baseDirectory = Get-Location

# Create the base folder
$baseFolderPath = Join-Path -Path $baseDirectory -ChildPath $baseFolderName
New-Item -Path $baseFolderPath -ItemType Directory

# Define the folder names and file names
$actions = "Create", "Delete", "Update", "Read"

# Create the folder structure within the base folder
foreach ($action in $actions) {
    $actionName = "${action}${name}"
    $actionDirectory = Join-Path -Path $baseFolderPath -ChildPath $actionName
    New-Item -Path $actionDirectory -ItemType Directory

    foreach ($fileType in "Handler", "Mapper", "Validation") {
        $filename = "${actionName}${fileType}.cs"
        New-Item -Path (Join-Path -Path $actionDirectory -ChildPath $filename) -ItemType File
    }

    $recordsDirectory = Join-Path -Path $actionDirectory -ChildPath "Records"
    New-Item -Path $recordsDirectory -ItemType Directory

    foreach ($recordType in "Request", "Response") {
        $filename = "${actionName}${recordType}.cs"
        New-Item -Path (Join-Path -Path $recordsDirectory -ChildPath $filename) -ItemType File
    }
}

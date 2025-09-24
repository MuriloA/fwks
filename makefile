PROJECT_NAME := fwksLabs
NUGET_SOURCE_URL := https://nuget.pkg.github.com/MuriloA/index.json
NUGET_SOURCE_NAME := "$(PROJECT_NAME)"
OUTPUT_DIR := "$(USERPROFILE)\.artifacts\$(PROJECT_NAME)"
VERSION := 1.0.0-dev.$(shell date +"%y%m%d%H%M%3N")

test:
	@echo $(shell date +"%y%m%d%H%M%3N")

source-local:
	@echo "Adding local source to Nuget.config" && \
	dotnet nuget add source $(OUTPUT_DIR) --name $(NUGET_SOURCE_NAME).local

source-remote:
	@echo "Adding remote source to Nuget.config" && \
	dotnet nuget add source $(NUGET_SOURCE_URL) --name $(NUGET_SOURCE_NAME) --username USR --password PWD 

pack:
	@rm $(OUTPUT_DIR) -rdf && mkdir -p $(OUTPUT_DIR)
	@echo "Packing the project $(VERSION)" && \
	dotnet pack --configuration Release --output $(OUTPUT_DIR) -p:Version=$(VERSION)

push: pack
	@echo "Pushing packages to Github Artifacts"
	dotnet nuget push $(OUTPUT_DIR)/*.nupkg --source $(NUGET_SOURCE_NAME) --api-key PWD --skip-duplicate
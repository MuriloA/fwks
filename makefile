PROJECT_NAME := fwksLabs
NUGET_SOURCE_NAME := "$(PROJECT_NAME).local"
OUTPUT_DIR := "$(USERPROFILE)\.artifacts\$(PROJECT_NAME)"
VERSION := 1.0.0-dev.$(shell date +"%y%m%d%H%M%3N")

test:
	@echo $(shell date +"%y%m%d%H%M%3N")

source-add:
	@echo "Adding local source to Nuget.config" && \
	dotnet nuget add source $(OUTPUT_DIR) --name $(NUGET_SOURCE_NAME)

libs-pack:
	@rm $(OUTPUT_DIR) -rdf && mkdir -p $(OUTPUT_DIR)
	@echo "Packing the project $(VERSION)" && \
	dotnet pack --configuration Release --output $(OUTPUT_DIR) -p:Version=$(VERSION)

cli-install:
	$(MAKE) libs-pack
	@echo "Removing old CLI tool (if any)" && \
	dotnet tool uninstall --global $(PROJECT_NAME).cli || true
	@echo "Installing CLI" && \
	dotnet tool install --global $(PROJECT_NAME).cli --add-source $(OUTPUT_DIR) --version 1.0.0
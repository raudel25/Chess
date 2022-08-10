.PHONY: dev
dev:
	dotnet watch run --project ChessServer

.PHONY: test
test:
	dotnet run --project Test

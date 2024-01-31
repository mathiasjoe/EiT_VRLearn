# Git Commands

## Workflow får å gjøre nye endringer

1. Gå til main og last ned nyeste endringer

```
git checkout origin main
git pull
```

2. Lag nye branch

```
git checkout -b <navn_på_branch>
```

3. Lagre endringer på den nye branchen

```
git add .
git commit -m "<kort beskrivelse>"
```

4. Push endringer til github

```
git push
```

5. Når du er ferdig kan du merge endringene din med main branchen ved å gå til github repoet, trykke på den grønne knappen "Compare and merge", så lage en merge request ved å trykke på "Create merge request". Hvis det er noen konflikter mellom din branch og main branchen må du fikse opp i det før du får merga. Merge ved å trykke på "merge" knappen.

## Gå til en branch og se nyeste endringer

1. Gå til branch

```
git checkout <branch_navn>
```

2. Laste ned nyeste endringer

```
git pull
```

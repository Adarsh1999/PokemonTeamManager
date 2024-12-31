{{- define "postgresql.env" }}
- name: ALLOW_EMPTY_PASSWORD
  value: "yes"
- name: POSTGRESQL_USERNAME
  valueFrom:
    configMapKeyRef:
      name: {{ .Release.Name }}-config
      key: POSTGRESQL_USERNAME
- name: POSTGRESQL_PASSWORD
  valueFrom:
    secretKeyRef:
      name: {{ .Release.Name }}-secrets
      key: POSTGRESQL_PASSWORD
- name: POSTGRESQL_DATABASE
  valueFrom:
    configMapKeyRef:
      name: {{ .Release.Name }}-config
      key: POSTGRESQL_DATABASE
{{- end }}

{{- define "api.env" }}
- name: ASPNETCORE_ENVIRONMENT
  valueFrom:
    configMapKeyRef:
      name: {{ .Release.Name }}-config
      key: ASPNETCORE_ENVIRONMENT
- name: ConnectionStrings__DefaultConnection
  value: "Host={{ .Release.Name }}-postgresql;Database=pokemondb;Username=postgres;Password=postgres"
{{- end }}

{{- define "migrations.env" }}
- name: ASPNETCORE_ENVIRONMENT
  valueFrom:
    configMapKeyRef:
      name: {{ .Release.Name }}-config
      key: ASPNETCORE_ENVIRONMENT
- name: ConnectionStrings__DefaultConnection
  value: "Host={{ .Release.Name }}-postgresql;Database=pokemondb;Username=postgres;Password=postgres"
{{- end }}
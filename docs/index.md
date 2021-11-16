# Exercises

{% for exercise in site.exercises %}
-   [{{ exercise.title }}](<{{ exercise.url  | relative_url }}>)
{% endfor %}

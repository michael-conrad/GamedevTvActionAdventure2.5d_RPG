shader_type spatial;

render_mode unshaded, cull_disabled;

uniform bool active = false;
uniform vec4 flash_color: source_color = vec4(1.0, 0.2, 0.2, 1.0);
uniform sampler2D tex: source_color;

void fragment() {
	if (active) {
		vec4 color = texture(tex, UV);
		ALPHA = color.a;
		ALBEDO = vec3(flash_color.r, flash_color.g, flash_color.b);
	} else {
		vec4 color = texture(tex, UV);
		ALPHA = color.a;
		ALBEDO = vec3(color.r, color.g, color.b);
	}
}



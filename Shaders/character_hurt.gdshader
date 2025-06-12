shader_type spatial;

render_mode unshaded, cull_disabled, depth_draw_opaque, depth_prepass_alpha;

uniform bool active = false;
uniform vec4 flash_color: source_color = vec4(1.0, 0.8, 0.8, 1.0);
uniform sampler2D tex: source_color;

void fragment() {
	vec4 color = texture(tex, UV);

	ALPHA = color.a;
	if (active == true) {
		color = flash_color;
	}

	ALBEDO = vec3(color.r, color.g, color.b);
}



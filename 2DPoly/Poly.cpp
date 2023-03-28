#include "Poly.h"

PolyObject::PolyObject()
{
	
}

PolyObject::~PolyObject()
{
}

void PolyObject::addVertex(vec2 p_vert)
{

	vertices.push_back(p_vert);
}

void PolyObject::setColor(vec3 p_color)
{
	color = p_color;
}

unsigned int PolyObject::getVertNum()
{
	return vertices.size();
}

void PolyObject::draw()
{
	glColor3f(color.r, color.g, color.b);
	if (vertices.size() == 1) {
		glBegin(GL_POINTS);
	}
	else if (vertices.size() == 2) {
		glBegin(GL_LINES);
	}
	else 
		glBegin(GL_POLYGON);
	for (int i = 0; i < vertices.size(); i++)
		glVertex2f(vertices[i].x, vertices[i].y);
	glEnd();
}

void PolyObject::draw(vec2 p_mousePos)
{
	glColor3f(color.r, color.g, color.b);
	if (vertices.size() < 2) {
		glBegin(GL_LINE_STRIP);
	}
	else {
		glBegin(GL_POLYGON);
	}
	for (int i = 0; i < vertices.size(); i++)
		glVertex2f(vertices[i].x, vertices[i].y);
	glVertex2f(p_mousePos.x, p_mousePos.y);
	glEnd();
}
void PolyObject::clearVertices()
{
	vertices.clear();
}

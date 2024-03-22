using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodePulse.API.Database;
using CodePulse.API.Models;
using CodePulse.API.DTOs.BlogPost;
using CodePulse.API.DTOs.Category;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogPostsController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<List<BlogPostDto>> GetBlogPostsAsync()
        {
            var blogPosts = await _context.BlogPost
                .Include(x => x.Categories) 
                .Select(x => new BlogPostDto
                {
                    Author = x.Author,
                    Categories = x.Categories.Select(c => new CategoryListDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        UrlHandle = c.UrlHandle
                    }).ToList(),
                    Content = x.Content,
                    FeaturedImageUrl = x.FeaturedImageUrl,
                    Id = x.Id,
                    IsVisible = x.IsVisible,
                    PublishedDate = x.PublishedDate,
                    ShortDescription = x.ShortDescription,
                    Title = x.Title,
                    UrlHandle = x.UrlHandle
                })
                .ToListAsync(); 

            return blogPosts;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetBlogPost(Guid id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return new BlogPostDto
            {
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Id = blogPost.Id,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(c => new CategoryListDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList(),
            };
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(Guid id, BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return BadRequest();
            }

            _context.Entry(blogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

     
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPost)
        {
            _context.BlogPost.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogPost", new { id = blogPost.Id }, blogPost);
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.BlogPost.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(Guid id)
        {
            return _context.BlogPost.Any(e => e.Id == id);
        }
    }
}

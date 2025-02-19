﻿using System.Threading;
using System.Threading.Tasks;
using Args = System.Collections.Generic.Dictionary<string, object>;

namespace SlackNet.WebApi
{
    public interface IStarsApi
    {
        /// <summary>
        /// Adds a star to a file.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.add">Slack documentation</a> for more information.</remarks>
        /// <param name="fileId">File to add star to.</param>
        /// <param name="cancellationToken"></param>
        Task AddToFile(string fileId, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Adds a star to a file comment.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.add">Slack documentation</a> for more information.</remarks>
        /// <param name="fileCommentId">File comment to add star to.</param>
        /// <param name="cancellationToken"></param>
        Task AddToFileComment(string fileCommentId, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Adds a star to a channel.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.add">Slack documentation</a> for more information.</remarks>
        /// <param name="channelId">Channel, private group, or DM to add star to.</param>
        /// <param name="cancellationToken"></param>
        Task AddToChannel(string channelId, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Adds a star to a message.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.add">Slack documentation</a> for more information.</remarks>
        /// <param name="channelId">Channel where the message to add star to was posted.</param>
        /// <param name="ts">Timestamp of the message to add star to.</param>
        /// <param name="cancellationToken"></param>
        Task AddToMessage(string channelId, string ts, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Lists the items starred by the authed user.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.list">Slack documentation</a> for more information.</remarks>
        /// <param name="count">Number of items to return per page.</param>
        /// <param name="page">Page number of results to return.</param>
        /// <param name="cursor">
        /// Parameter for pagination.
        /// Set cursor equal to the <see cref="ResponseMetadata.NextCursor"/> returned by the previous request's <see cref="StarListResponse.ResponseMetadata"/>.
        /// </param>
        /// <param name="cancellationToken"></param>
        Task<StarListResponse> List(int count = 100, int page = 1, string cursor = null, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Removes a star from a file.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.remove">Slack documentation</a> for more information.</remarks>
        /// <param name="fileId">File to remove star from.</param>
        /// <param name="cancellationToken"></param>
        Task RemoveFromFile(string fileId, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Removes a star from a file comment.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.remove">Slack documentation</a> for more information.</remarks>
        /// <param name="fileCommentId">File comment to remove star from.</param>
        /// <param name="cancellationToken"></param>
        Task RemoveFromFileComment(string fileCommentId, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Removes a star from a channel.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.remove">Slack documentation</a> for more information.</remarks>
        /// <param name="channelId">Channel, private group, or DM to remove star from.</param>
        /// <param name="cancellationToken"></param>
        Task RemoveFromChannel(string channelId, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Removes a star from a message.
        /// </summary>
        /// <remarks>See the <a href="https://api.slack.com/methods/stars.remove">Slack documentation</a> for more information.</remarks>
        /// <param name="channelId">Channel where the message to remove star from was posted.</param>
        /// <param name="ts">Timestamp of the message to remove star from.</param>
        /// <param name="cancellationToken"></param>
        Task RemoveFromMessage(string channelId, string ts, CancellationToken? cancellationToken = null);
    }

    public class StarsApi : IStarsApi
    {
        private readonly ISlackApiClient _client;
        public StarsApi(ISlackApiClient client) => _client = client;

        public Task AddToFile(string fileId, CancellationToken? cancellationToken = null) =>
            _client.Post("stars.add", new Args { { "file", fileId } }, cancellationToken);

        public Task AddToFileComment(string fileCommentId, CancellationToken? cancellationToken = null) =>
            _client.Post("stars.add", new Args { { "file_comment", fileCommentId } }, cancellationToken);

        public Task AddToChannel(string channelId, CancellationToken? cancellationToken = null) =>
            _client.Post("stars.add", new Args { { "channel", channelId } }, cancellationToken);

        public Task AddToMessage(string channelId, string ts, CancellationToken? cancellationToken = null) =>
            _client.Post("stars.add", new Args
                {
                    { "channel", channelId },
                    { "timestamp", ts }
                }, cancellationToken);

        public Task<StarListResponse> List(int count = 100, int page = 1, string cursor = null, CancellationToken? cancellationToken = null) =>
            _client.Get<StarListResponse>("stars.list", new Args
                {
                    { "count", count },
                    { "page", page },
                    { "cursor", cursor }
                }, cancellationToken);

        public Task RemoveFromFile(string fileId, CancellationToken? cancellationToken = null) =>
            _client.Post("stars.remove", new Args { { "file", fileId } }, cancellationToken);

        public Task RemoveFromFileComment(string fileCommentId, CancellationToken? cancellationToken = null) =>
            _client.Post("stars.remove", new Args { { "file_comment", fileCommentId } }, cancellationToken);

        public Task RemoveFromChannel(string channelId, CancellationToken? cancellationToken = null) =>
            _client.Post("stars.remove", new Args { { "channel", channelId } }, cancellationToken);

        public Task RemoveFromMessage(string channelId, string ts, CancellationToken? cancellationToken = null) =>
            _client.Post("stars.remove", new Args
                {
                    { "channel", channelId },
                    { "timestamp", ts }
                }, cancellationToken);
    }
}